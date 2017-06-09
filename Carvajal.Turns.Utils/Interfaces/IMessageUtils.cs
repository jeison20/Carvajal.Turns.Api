namespace Carvajal.Turns.Utils.Interfaces
{
    public interface IMessageUtils
    {
        /// <summary>
        /// Metodo que realiza la busqueda de la plantilla html MailRecoveryPasswor para la recuperacion de contraseña almacenada en un recurso para su uso en un email
        /// </summary>
        /// <param name="userName">nombre del usuario </param>
        /// <param name="codeRecovery">codigo de recuperacion de contraseña</param>
        /// <returns>retorna un html</returns>
        string GetTemplateMailRecoveryPasswor(string userName, string codeRecovery);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantName"></param>
        /// <param name="manufacturerName"></param>
        /// <param name="codeRecovery"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        string GetTemplateMailCreateMerchantInitial(string merchantName, string manufacturerName, string codeRecovery, string country);
        /// <summary>
        /// Metodo que realiza la busqueda de la plantilla html ChangeMail para recordatorio de usuario y contraseña al cambiar de email almacenada en un recurso para su uso en un email
        /// </summary>
        /// <param name="UserName">usuario identificacion</param>
        /// <param name="User">Pk de identificacion del usuario</param>
        /// <param name="Password">contraseña almacenada</param>
        /// <param name="Country">Pais asociado a la compañia del usuario </param>
        /// <param name="Comerce">Nombre de la empresa a la que se encuentra vinculado el usuario</param>
        /// <returns>Retorna un html</returns>
        string GetTemplateChangeMail(string UserName, string User, string Password, string Country, string Comerce);

        /// <summary>
        /// Metodo que realiza la busqueda de la plantilla html para la creacion de nuevo  usuario almacenada en un recurso para su uso en un email
        /// </summary>
        /// <param name="UserName">usuario identificacion</param>
        /// <param name="User">Pk de identificacion del usuario</param>
        /// <param name="Password">contraseña almacenada</param>
        /// <param name="Country">Pais asociado a la compañia del usuario </param>
        /// <param name="Comerce">Nombre de la empresa a la que se encuentra vinculado el usuario</param>
        /// <returns>Retorna un html</returns>
        string GetTemplateNewUserMail(string UserName, string User, string Password, string Country, string Comerce);
    }
}