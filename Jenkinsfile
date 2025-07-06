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
                        mkdir -p /var/jenkins_home/tools
                        if ! command -v dotnet-sonarscanner >/dev/null 2>&1; then
                            echo "⚙️ Instalando dotnet-sonarscanner versión 10.2.0.117568..."
                            wget https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/10.2.0.117568/sonar-scanner-10.2.0.117568-net.zip
                            unzip sonar-scanner-10.2.0.117568-net.zip -d /var/jenkins_home/tools/sonar-scanner-dotnet
                            ln -s /var/jenkins_home/tools/sonar-scanner-dotnet/dotnet-sonarscanner /usr/local/bin/dotnet-sonarscanner
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
                echo "🚧 Restaurando dependencias..."
                sh 'dotnet restore'
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
                    echo "🔎 Validando artefactos en bin/Release/net8.0..."
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
                echo "🔧 Simulando despliegue en pruebas..."
                sh """
                    mkdir -p ${DEPLOY_TEST_PATH}
                    cp -r bin/Release/net8.0/* ${DEPLOY_TEST_PATH}/
                    echo '✅ Despliegue en pruebas completado.'
                """
            }
        }

        stage('Escaneo de Seguridad - Trivy') {
            steps {
                script {
                    echo "🛡️ Ejecutando escaneo de vulnerabilidades con Trivy..."
                    sh 'trivy fs . > trivy_report.txt || true'

                    def highCount = sh(script: "grep -c HIGH trivy_report.txt || true", returnStdout: true).trim()
                    def criticalCount = sh(script: "grep -c CRITICAL trivy_report.txt || true", returnStdout: true).trim()

                    echo "⚠️ Vulnerabilidades ALTAS: ${highCount}"
                    echo "⚠️ Vulnerabilidades CRÍTICAS: ${criticalCount}"
                }
            }
        }

        stage('Despliegue Simulado en Entorno de Producción') {
            steps {
                echo "🚀 Simulando despliegue en producción..."
                sh """
                    mkdir -p ${DEPLOY_PROD_PATH}
                    cp -r bin/Release/net8.0/* ${DEPLOY_PROD_PATH}/
                    echo '✅ Despliegue en producción completado.'
                """
            }
        }
    }

    post {
        always {
            echo "📢 Publicando reporte Trivy como artefacto..."
            archiveArtifacts artifacts: 'trivy_report.txt', fingerprint: true
            echo "✅ Pipeline finalizado. Revisa SonarQube y los artefactos."
        }
    }
}
