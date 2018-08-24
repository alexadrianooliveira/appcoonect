using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConectaLoja.BLL
{
    public class Security
    {
        //private string URL = "http://localhost:1000/";
        private string URL = Utils.Constantes.URL;
        private string username = Utils.Constantes.USERAPI;
        private string pwdAPI = Utils.Constantes.PWDAPI;

        private string GetToken(string usuario, string senha)
        {
            string url = URL + "/token";

            NameValueCollection parametros = new NameValueCollection();
            parametros.Add("username", usuario);
            parametros.Add("password", senha);
            parametros.Add("client_id", username);
            parametros.Add("client_secret", pwdAPI);
            parametros.Add("grant_type", "password");

            return Utils.Json.GetJSONString(url, parametros);
        }

        /// <summary>
        /// Valida para ver se o e-mail e a senha digitada pelo lojista é valido
        /// </summary>
        /// <param name="usuario">E-mail de acesso do usuário master</param>
        /// <param name="senha">Senha</param>
        /// <returns>ID do usuário Master, se devolver zero indica que os dados não conferem</returns>
        public int checkStore(string usuario, string senha)
        {
            string token = GetToken(usuario, senha);
            bool erro = token.IndexOf("error") >= 0;

            if (erro)
                return 0;
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                Models.Token obj = serializer.Deserialize<Models.Token>(token);
                obj.DataExpira = DateTime.Now.AddSeconds(obj.expires_in);

                int masterID = Utils.Valida.LimpaString(obj.masterid);
                return masterID;
            }
        }
    }
}
