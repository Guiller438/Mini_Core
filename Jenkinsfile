pipeline {
    agent any

    environment {
        DOTNET_ROOT = '/opt/dotnet'
        PATH = "${env.PATH}:${DOTNET_ROOT}:/opt/sonar-scanner/bin:/usr/local/bin"

        SONAR_HOST_URL = 'http://host.docker.internal:9000'
        DEPLOY_TEST_PATH = '/var/jenkins_home/deploy_test'
        DEPLOY_PROD_PATH = '/var/jenkins_home/deploy_prod_sim'
    }

    stages {

        stage('Preparar herramientas') {
            steps {
                script {
                    echo "🔧 Verificando instalación de dotnet-sonarscanner..."
                    sh '''
                        if ! command -v dotnet-sonarscanner >/dev/null 2>&1; then
                            echo "⚙️ Instalando dotnet-sonarscanner..."
                            wget https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/10.3.0.78115/sonar-scanner-msbuild-10.3.0.78115-net.zip
                            unzip sonar-scanner-msbuild-10.3.0.78115-net.zip -d /opt/sonar-scanner-dotnet
                            ln -s /opt/sonar-scanner-dotnet/dotnet-sonarscanner /usr/local/bin/dotnet-sonarscanner
                        fi
                        dotnet-sonarscanner --version
                    '''
                }
            }
        }

        stage('Checkout - Descargar Código') {
            steps {
                git branch: 'main', url: 'https://github.com/Guiller438/Mini_Core.git'
            }
        }

        stage('Restaurar Dependencias') {
            steps {
                script {
                    echo "🚧 Restaurando dependencias..."
                    sh 'dotnet restore'
                }
            }
        }

        stage('Análisis de SonarQube - Calidad y Seguridad') {
            steps {
                withCredentials([string(credentialsId: 'SONARQUBE_TOKEN', variable: 'SONAR_TOKEN')]) {
                    script {
                        echo "🔍 Iniciando análisis de SonarQube..."
                        sh """
                            dotnet-sonarscanner begin /k:"ProyectoFinalPS" /d:sonar.host.url=${SONAR_HOST_URL} /d:sonar.login=${SONAR_TOKEN}
                            dotnet build --configuration Release
                            dotnet-sonarscanner end /d:sonar.login=${SONAR_TOKEN}
                        """
                    }
                }
            }
        }

        stage('Validación de Artefactos') {
            steps {
                script {
                    echo "🔎 Validando existencia de artefactos generados..."
                    sh '''
                        if [ ! -d bin/Release/net8.0 ] || [ -z "$(ls -A bin/Release/net8.0/*.dll 2>/dev/null)" ]; then
                            echo '⚠️ No se generaron artefactos. Fallando el pipeline.'
                            exit 1
                        fi
                    '''
                    echo "✅ Artefactos generados correctamente."
                }
            }
        }

        stage('Despliegue Simulado en Entorno de Pruebas') {
            steps {
                script {
                    echo "🔧 Simulando despliegue en entorno de pruebas..."
                    sh """
                        mkdir -p ${DEPLOY_TEST_PATH}
                        cp -r bin/Release/net8.0/* ${DEPLOY_TEST_PATH}/
                        echo '✅ Despliegue en entorno de pruebas completado.'
                    """
                }
            }
        }

        stage('Escaneo de Seguridad - Trivy') {
            steps {
                script {
                    echo "🛡️ Ejecutando escaneo de vulnerabilidades con Trivy..."
                    sh 'trivy fs . > trivy_report.txt || true'

                    def highCount = sh(script: "grep -c HIGH trivy_report.txt || true", returnStdout: true).trim()
                    def criticalCount = sh(script: "grep -c CRITICAL trivy_report.txt || true", returnStdout: true).trim()

                    echo "⚠️ Resumen de vulnerabilidades:"
                    echo "🔴 Vulnerabilidades ALTAS: ${highCount}"
                    echo "🔴 Vulnerabilidades CRÍTICAS: ${criticalCount}"
                    echo "📄 Reporte completo disponible como artefacto."
                }
            }
        }

        stage('Despliegue Simulado en Entorno de Producción') {
            steps {
                script {
                    echo "🚀 Simulando despliegue en entorno de producción..."
                    sh """
                        mkdir -p ${DEPLOY_PROD_PATH}
                        cp -r bin/Release/net8.0/* ${DEPLOY_PROD_PATH}/
                        echo '✅ Despliegue en entorno de producción completado.'
                    """
                }
            }
        }
    }

    post {
        always {
            echo "📢 Publicando reporte de Trivy como artefacto descargable..."
            archiveArtifacts artifacts: 'trivy_report.txt', fingerprint: true
            echo "✅ Pipeline finalizado. Revisa los artefactos y el tablero de SonarQube."
        }
    }
}
