pipeline {
    agent any

    environment {
        SONAR_SCANNER_DIR = '/var/jenkins_home/tools/sonar-scanner-dotnet'
    }

    stages {

        stage('Preparar herramientas') {
            steps {
                script {
                    echo "🔧 Preparando SonarScanner para .NET..."
                    sh '''
                        mkdir -p ${SONAR_SCANNER_DIR}
                        
                        if [ ! -f ${SONAR_SCANNER_DIR}/SonarScanner.MSBuild.dll ]; then
                            echo "⚙️ Instalando SonarScanner .NET 10.2.0.117568..."
                            wget -q https://github.com/SonarSource/sonar-scanner-msbuild/releases/download/10.2.0.117568/sonar-scanner-10.2.0.117568-net.zip
                            unzip -o sonar-scanner-10.2.0.117568-net.zip -d ${SONAR_SCANNER_DIR}
                        fi

                        chmod +x ${SONAR_SCANNER_DIR}/sonar-scanner-5.0.2.4997/bin/sonar-scanner
                    '''
                }
            }
        }

        stage('Checkout - Descargar Código') {
            steps {
                echo '⬇️ Descargando código fuente...'
                git branch: 'main', url: 'https://github.com/Guiller438/Mini_Core.git'
            }
        }

        stage('Restaurar Dependencias') {
            steps {
                echo '🚧 Restaurando dependencias...'
                sh 'dotnet restore'
            }
        }

        stage('Análisis de SonarQube - Calidad y Seguridad') {
            environment {
                SONAR_TOKEN = credentials('SONAR_TOKEN')  // Debes tener SONAR_TOKEN configurado en Jenkins
            }
            steps {
                script {
                    echo "🔍 Iniciando análisis de SonarQube..."
                    sh '''
                        dotnet ${SONAR_SCANNER_DIR}/SonarScanner.MSBuild.dll begin /k:ProyectoFinalPS /d:sonar.host.url=http://host.docker.internal:9000 /d:sonar.login=${SONAR_TOKEN}
                        dotnet build --configuration Release
                        dotnet ${SONAR_SCANNER_DIR}/SonarScanner.MSBuild.dll end /d:sonar.login=${SONAR_TOKEN}
                    '''
                }
            }
        }

        stage('Validación de Artefactos') {
            steps {
                echo '🔍 Validando artefactos generados...'
                sh 'ls -lh bin/Release/net8.0/'
            }
        }

        stage('Despliegue Simulado en Entorno de Pruebas') {
            steps {
                echo '🚀 Despliegue de prueba completado (simulado).'
            }
        }

        stage('Escaneo de Seguridad - Trivy') {
            steps {
                echo '🔐 Escaneando con Trivy (simulado)...'
                // Aquí iría el escaneo real si tienes Trivy instalado
            }
        }

        stage('Despliegue Simulado en Entorno de Producción') {
            steps {
                echo '🚀 Despliegue de producción completado (simulado).'
            }
        }
    }

    post {
        always {
            echo "📢 Publicando reporte Trivy como artefacto..."
            archiveArtifacts artifacts: '**/trivy-report.txt', allowEmptyArchive: true
            echo "✅ Pipeline finalizado. Revisa SonarQube y los artefactos."
        }
    }
}
