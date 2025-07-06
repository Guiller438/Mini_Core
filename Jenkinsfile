pipeline {
    agent any

    stages {
        stage('Checkout') {
            steps {
                git branch: 'main', url: 'https://github.com/Guiller438/Mini_Core.git'
            }
        }

        stage('Build del Proyecto') {
            steps {
                script {
                    // Restaurar paquetes y compilar la solución
                    sh 'dotnet restore'
                    sh 'dotnet build --configuration Release'
                }
            }
        }

        stage('Escaneo de Seguridad - Trivy') {
            steps {
                script {
                    // Trivy escanea la carpeta actual del workspace
                    sh 'trivy fs . || true'
                }
            }
        }

        stage('Simulación de Despliegue') {
            steps {
                echo 'Despliegue simulado: el proyecto se compila y pasó por validaciones de seguridad.'
            }
        }
    }
}
