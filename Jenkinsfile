pipeline {
    agent any

    environment {
        DOTNET_ROOT = '/opt/dotnet'
        PATH = "${env.PATH}:${DOTNET_ROOT}"
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

                    // Aquí NO detenemos el pipeline, solo informamos
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
            echo "✅ Pipeline finalizado. Revisa los artefactos para ver el análisis de Trivy."
        }
    }
}
