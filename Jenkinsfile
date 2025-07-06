pipeline {
    agent any

    environment {
        DOTNET_ROOT = '/opt/dotnet'
        PATH = "${env.PATH}:${DOTNET_ROOT}"
        SONAR_HOST_URL = 'http://host.docker.internal:9000' // Cambia si SonarQube está en otro lado
    }

    stages {

        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Guiller438/Mini_Core.git'
            }
        }

        stage('Análisis de SonarQube') {
            steps {
                withCredentials([string(credentialsId: 'SONARQUBE_TOKEN', variable: 'SONAR_TOKEN')]) {
                    script {
                        echo "🔍 Iniciando análisis de SonarQube..."
                        sh """
                            dotnet sonarscanner begin /k:"ProyectoFinalPS" /d:sonar.host.url=${SONAR_HOST_URL} /d:sonar.login=${SONARQUBE_TOKEN}
                            dotnet build --configuration Release
                            dotnet sonarscanner end /d:sonar.login=${SONARQUBE_TOKEN}
                        """
                    }
                }
            }
        }

        stage('Build del Proyecto') {
            steps {
                script {
                    echo "🚧 Restaurando dependencias..."
                    sh 'dotnet restore'

                    echo "🛠️ Compilando proyecto en modo Release..."
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Despliegue en Entorno de Pruebas') {
            steps {
                script {
                    echo "🔧 Simulando despliegue en entorno de pruebas..."
                    sh '''
                        mkdir -p /var/jenkins_home/deploy_test
                        cp -r bin/Release/net8.0/* /var/jenkins_home/deploy_test/
                        echo "✅ Despliegue simulado en entorno de pruebas completado."
                    '''
                }
            }
        }

        stage('Escaneo de Seguridad - Trivy') {
            steps {
                script {
                    echo "🔎 Ejecutando análisis de seguridad con Trivy..."
                    sh 'trivy fs . > trivy_report.txt || true'

                    def highCount = sh(script: "grep -c HIGH trivy_report.txt || true", returnStdout: true).trim()
                    def criticalCount = sh(script: "grep -c CRITICAL trivy_report.txt || true", returnStdout: true).trim()

                    echo "⚠️ Resumen de vulnerabilidades detectadas:"
                    echo "🔴 ALTAS: ${highCount}"
                    echo "🔴 CRÍTICAS: ${criticalCount}"
                    echo "📄 Reporte completo disponible como artefacto."
                }
            }
        }

        stage('Despliegue en Entorno de Producción Simulado') {
            steps {
                script {
                    echo "🚀 Simulando despliegue en entorno de producción..."
                    sh '''
                        mkdir -p /var/jenkins_home/deploy_prod_sim
                        cp -r bin/Release/net8.0/* /var/jenkins_home/deploy_prod_sim/
                        echo "✅ Despliegue simulado en entorno de producción completado."
                    '''
                }
            }
        }
    }

    post {
        always {
            echo "📢 Guardando reporte de Trivy como artefacto..."
            archiveArtifacts artifacts: 'trivy_report.txt', fingerprint: true
            echo "✅ Pipeline finalizado. Revisa los artefactos y el tablero de SonarQube."
        }
    }
}
