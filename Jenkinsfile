pipeline {
    agent any

    environment {
        DOTNET_ROOT = '/opt/dotnet'
        SONAR_SCANNER_DIR = '/var/jenkins_home/tools/sonar-scanner-dotnet'
        PATH = "${env.PATH}:${DOTNET_ROOT}:${SONAR_SCANNER_DIR}/sonar-scanner-5.0.2.4997/bin:/var/jenkins_home/.dotnet/tools"

        SONAR_HOST_URL = 'http://host.docker.internal:9000'    
        DEPLOY_TEST_PATH = '/var/jenkins_home/deploy_test'
        DEPLOY_PROD_PATH = '/var/jenkins_home/deploy_prod_sim'
    }

    stages {

        stage('Preparar herramientas') {
            steps {
                script {
                    echo "🔧 Verificando instalación de SonarScanner para .NET..."
                    sh '''
                        mkdir -p ${SONAR_SCANNER_DIR}
                        if [ ! -f ${SONAR_SCANNER_DIR}/SonarScanner.MSBuild.dll ]; then
                            echo "⚙️ Instalando SonarScanner .NET versión 10.2.0.117568..."
                            wget https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/10.2.0.117568/sonar-scanner-10.2.0.117568-net.zip
                            unzip -o sonar-scanner-10.2.0.117568-net.zip -d ${SONAR_SCANNER_DIR}
                            chmod +x ${SONAR_SCANNER_DIR}/sonar-scanner-5.0.2.4997/bin/sonar-scanner
                        fi
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
                        sh '''
                            dotnet ${SONAR_SCANNER_DIR}/SonarScanner.MSBuild.dll begin /k:"ProyectoFinalPS" /d:sonar.host.url=${SONAR_HOST_URL} /d:sonar.login=${SONAR_TOKEN}
                            dotnet build --configuration Release
                            dotnet ${SONAR_SCANNER_DIR}/SonarScanner.MSBuild.dll end /d:sonar.login=${SONAR_TOKEN}
                        '''
                    }
                }
            }
        }

        stage('Validación de Artefactos') {
            steps {
                script {
                    echo "🔎 Validando artefactos..."
                    sh '''
                        if [ -z "$(ls -A bin/Release/net8.0/*.dll 2>/dev/null)" ]; then
                            echo "⚠️ No se generaron artefactos. Fallando el pipeline."
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
                    echo "🚀 Desplegando en entorno de pruebas..."
                    sh '''
                        mkdir -p ${DEPLOY_TEST_PATH}
                        cp -r bin/Release/net8.0/* ${DEPLOY_TEST_PATH}/
                        echo "✅ Despliegue completado en pruebas."
                    '''
                }
            }
        }

        stage('Escaneo de Seguridad - Trivy') {
            steps {
                script {
                    echo "🛡️ Ejecutando Trivy..."
                    sh '''
                        trivy fs . > trivy_report.txt || true
                        grep -c HIGH trivy_report.txt || true
                        grep -c CRITICAL trivy_report.txt || true
                    '''
                    echo "⚠️ Revisa trivy_report.txt para detalles."
                }
            }
        }

        stage('Despliegue Simulado en Entorno de Producción') {
            steps {
                script {
                    echo "🚀 Desplegando en entorno de producción..."
                    sh '''
                        mkdir -p ${DEPLOY_PROD_PATH}
                        cp -r bin/Release/net8.0/* ${DEPLOY_PROD_PATH}/
                        echo "✅ Despliegue completado en producción."
                    '''
                }
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
