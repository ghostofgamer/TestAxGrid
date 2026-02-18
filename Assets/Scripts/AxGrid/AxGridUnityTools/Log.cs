using System;
using System.Text.RegularExpressions;
using log4net;

namespace AxGrid
{
    public class Log
    {
        // public static ILog logger = LogManager.GetLogger("Ax");

        private static readonly ILog logger = new LogWrapper(log4net.LogManager.GetLogger("Ax"));
        
        public static void Debug(string message)
        {
            logger.Debug(message);
        }

        public static void Info(string message)
        {
            logger.Info(message);
        }

        public static void Warn(string message)
        {
            logger.Warn(message);
        }

        public static void Error(string message)
        {
            logger.Error(message);
        }

        public static void Error(Exception ex)
        {
            logger.Error(ex.Message);
        }

        public static void Error(Exception ex, string message)
        {
            logger.Error($"{message}: {ex.Message}\n{ex.StackTrace}");
        }
        
        /*public static void Debug(string message)
        {
            if (logger.IsDebugEnabled)
                logger.Debug(message);
        }
        
        public static void Info(string message)
        {
            if (logger.IsInfoEnabled)
                logger.Info(message);
        }
        
        public static void Warn(string message)
        {
            if (logger.IsWarnEnabled)
                logger.Warn(message);
        }
        
        public static void Error(string message)
        {
            if (logger.IsErrorEnabled)
                logger.Error(message);
        }
        
        public static void Error(Exception ex)
        {
            if (logger.IsErrorEnabled)
                logger.Error(ex);
        }
        
        public static void Error(Exception ex, string message)
        {
            if (logger.IsErrorEnabled)
                logger.Error($"{message}: {ex.Message}\n{ex.StackTrace}");
        }*/
    }
}