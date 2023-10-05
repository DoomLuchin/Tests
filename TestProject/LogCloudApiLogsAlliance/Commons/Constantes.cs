namespace LogsAllianceApi.Commons
{
    public class Constantes
    {
        public static IConfiguration Configuration { get; set; }
    }

    public static class ServicioCore
    {
        public static string IssuerIdentity = "https://localhost:7002/";
        public static string PrefijoUrlCatalogos = "IdentitySSO/";
    }

    public static class AutorizacionSSO
    {
        public static string ScopeProveedores = "proveedores360Scope";
        public static string ScopeCliente360 = "client360scope";//Constantes.Configuration["Claims:Proveedores360"];
        public static string ScopeAlliance = "allianceScope";
        public static string ScopeLagDev = "lagDevScope";
        public static string AudienceProveedores = "ProveedoresApi";
        public static string AudienceAlliance = "AllianceClienteApi";
        public static string ClientId = "AllianceCliente";
        public static string Password = "131195";
        public static string ClientIdIntrospection = "AllianceClienteApi";
        public static string PasswordIntrospection = "api1234";
    }
    public static class Token
    {
        public static string ClientId = "proveedores360";
        public static string Password = "131195";
    }
    public class UsersStatus
    {
        public static string Activo = "ACTIVO";
        public static string Inactivo = "INACTIVO";
    }

    public class AccionMenu
    {
        public static string Escaner = "ESCANER";
    }

    public class UsersType
    {
        public static string Empleado = "EMPLEADO";
        public static string Exportador = "EXPORTADOR";
        public static string GrupoExportador = "GRUPO_EXPORTADOR";
        public static string GrupoCliente = "GRUPO CLIENTE";
        public static string Cliente = "CLIENTE";
    }

    public class MenuCoordinaciones
    {
        public static string Menu = "IdMenuProveedores360";
    }

    public class Empresas
    {
        public static string UIO = "EMP011";
    }
}
