# Proyecto Web API (.NET 8)

## 📋 Requisitos previos  
Asegúrate de tener instalados los siguientes componentes en tu equipo:  
- **.NET 8 SDK**: [Descargar .NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)  
- **SQL Server**: Instancia local o remota configurada.

---

## 🗄️ Restaurar la base de datos  
1. Copia el archivo de backup `TestDev2025BD_BackupFull.bak` proporcionado en tu servidor SQL.  
2. Restaura la base de datos
3. en appsettings.json actualiza el ConnectionStrings `Server=TU_SERVIDOR;Database=TestDev2025BD;Trusted_Connection=True;TrustServerCertificate=True;`
4. compila el proyecto y ejecutarlo, deberia abrir en https://localhost:7182
5. la url https://localhost:7182/swagger/index.html mostrará la documentación
