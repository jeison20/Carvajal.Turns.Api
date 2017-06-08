using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using log4net.Appender;

namespace Component
{
    public static class LogComponent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">nombre del log ha crear</param>
        /// <returns></returns>
        private static log4net.ILog GetLogger([CallerFilePath]string filename = "")
        {
            return log4net.LogManager.GetLogger(filename);
        }
        /// <summary>
        /// Metodo de escritura en el log
        /// </summary>
        /// <param name="MerchantId">empresa del usuario logueado</param>
        /// <param name="ProcessID">proceso que se estaba ejecutando</param>
        /// <param name="InfoMessage">mensaje informativo</param>
        public static void WriteLog(string MerchantId, string ProcessID, string InfoMessage)
        {
            log4net.ILog Log = GetLogger();
            Log.Info(string.Format("[{0}][{1}] {2}", MerchantId, ProcessID, InfoMessage));
        }
        /// <summary>
        /// Metodo de escritura en el log
        /// </summary>
        /// <param name="MerchantId">empresa del usuario logueado</param>
        /// <param name="ProcessID">proceso que se estaba ejecutando</param>
        /// <param name="InfoMessage">mensaje informativo</param>
        public static void WriteWarn(string MerchantId, string ProcessID, string InfoMessage)
        {
            log4net.ILog Log = GetLogger();
            Log.Warn(string.Format("[{0}][{1}] {2}", MerchantId, ProcessID, InfoMessage));
        }
        /// <summary>
        /// Metodo de escritura en el log
        /// </summary>
        /// <param name="MerchantId">empresa del usuario logueado</param>
        /// <param name="ProcessID">proceso que se estaba ejecutando</param>
        /// <param name="InfoMessage">mensaje informativo</param>
        public static void WriteError(string MerchantId, string ProcessID, string InfoMessage)
        {
            log4net.ILog Log = GetLogger();
            Log.Error(string.Format("[{0}][{1}] {2}", MerchantId, ProcessID, InfoMessage));
        }
    }
}
