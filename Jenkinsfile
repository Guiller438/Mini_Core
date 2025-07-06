pipeline {
    agent any

    environment {
        DOTNET_ROOT = "/opt/dotnet"
        PATH = "${env.PATH}:/opt/dotnet"
    }

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Guiller438/Mini_Core.git'
            }
        }

        stage('Build del Proyecto') {
            steps {
                script {
                    sh 'dotnet restore'
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Despliegue en Entorno de Pruebas') {
            steps {
                script {
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
                    sh 'trivy fs . > trivy_report.txt || true'
                    
                    def highVulns = sh(script: "grep -c 'HIGH' trivy_report.txt || true", returnStdout: true).trim()
                    def criticalVulns = sh(script: "grep -c 'CRITICAL' trivy_report.txt || true", returnStdout: true).trim()

                    echo "🔎 Vulnerabilidades ALTAS: ${highVulns}"
                    echo "🔎 Vulnerabilidades CRÍTICAS: ${criticalVulns}"

                    if (highVulns.toInteger() > 0 || criticalVulns.toInteger() > 0) {
                        error("❌ Se encontraron vulnerabilidades altas o críticas. Deteniendo el pipeline.")
                    }
                }
            }
        }

        stage('Despliegue en Entorno de Producción Simulado') {
            steps {
                script {
                    sh '''
                        mkdir -p /var/jenkins_home/deploy_prod
                        cp -r bin/Release/net8.0/* /var/jenkins_home/deploy_prod/
                        echo "🚀 Despliegue simulado en entorno de producción completado."
                    '''
                }
            }
        }
    }
}
