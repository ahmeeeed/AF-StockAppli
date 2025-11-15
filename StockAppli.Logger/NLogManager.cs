using NLog;
using System;

namespace StockAppli.Logger
{
    public static class NLogManager
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private static readonly object LockObj = new();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="foregroundColor"></param>
        public static void Trace(string message, string logClass = "", string logMethod = "", string foregroundColor = "")
        {
            lock (LockObj)
            {
                LogEventInfo eventInfo = new(LogLevel.Trace, "NLogManager", message)
                {
                    Properties =
                    {
                        ["error-class"] = logClass,
                        ["error-method"] = logMethod,
                        ["ForegroundColor"] = foregroundColor
                    }
                };

                Logger.Log(eventInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="foregroundColor"></param>
        public static void Debug(string message, string logClass = "", string logMethod = "", string foregroundColor = "")
        {
            lock (LockObj)
            {
                LogEventInfo eventInfo = new(LogLevel.Debug, "NLogManager", message)
                {
                    Properties =
                    {
                        ["error-class"] = logClass,
                        ["error-method"] = logMethod,
                        ["ForegroundColor"] = foregroundColor
                    }
                };

                Logger.Log(eventInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="foregroundColor"></param>
        public static void Info(string message, string logClass = "", string logMethod = "", string foregroundColor = "")
        {
            lock (LockObj)
            {
                LogEventInfo eventInfo = new(LogLevel.Info, "NLogManager", message)
                {
                    Properties =
                    {
                        ["error-class"] = logClass,
                        ["error-method"] = logMethod,
                        ["ForegroundColor"] = foregroundColor
                    }
                };

                Logger.Log(eventInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="foregroundColor"></param>
        public static void Warn(string message, string logClass = "", string logMethod = "", string foregroundColor = "")
        {
            lock (LockObj)
            {
                LogEventInfo eventInfo = new(LogLevel.Warn, "NLogManager", message)
                {
                    Properties =
                    {
                        ["error-class"] = logClass,
                        ["error-method"] = logMethod,
                        ["ForegroundColor"] = foregroundColor
                    }
                };

                Logger.Log(eventInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="foregroundColor"></param>
        public static void Error(string message, string logClass = "", string logMethod = "", string foregroundColor = "")
        {
            lock (LockObj)
            {
                LogEventInfo eventInfo = new(LogLevel.Error, "NLogManager", message)
                {
                    Properties =
                    {
                        ["error-class"] = logClass,
                        ["error-method"] = logMethod,
                        ["ForegroundColor"] = foregroundColor
                    }
                };

                Logger.Log(eventInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="foregroundColor"></param>
        public static void Error(string message, Exception ex, string logClass = "", string logMethod = "", string foregroundColor = "")
        {
            lock (LockObj)
            {
                LogEventInfo eventInfo = new(LogLevel.Error, "NLogManager", message)
                {
                    Properties =
                    {
                        ["error-class"] = logClass,
                        ["error-method"] = logMethod,
                        ["ForegroundColor"] = foregroundColor
                    }
                };

                if (ex != null)
                {
                    eventInfo.Properties["exception"] = ex.ToString();
                    eventInfo.Properties["error-source"] = ex.Source;
                    eventInfo.Properties["error-message"] = ex.Message;
                    eventInfo.Properties["inner-error-message"] = ex.InnerException;
                }

                Logger.Log(eventInfo);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="logClass"></param>
        /// <param name="logMethod"></param>
        /// <param name="ex"></param>
        /// <param name="foregroundColor"></param>
        public static void Fatal(string message, string logClass, string logMethod, Exception ex = null, string foregroundColor = "")
        {
            lock (LockObj)
            {
                LogEventInfo eventInfo = new(LogLevel.Fatal, "NLogManager", message)
                {
                    Properties =
                    {
                        ["error-class"] = logClass,
                        ["error-method"] = logMethod
                    }
                };
                if (ex != null)
                {
                    eventInfo.Properties["error-source"] = ex.Source;
                    eventInfo.Properties["error-message"] = ex.Message;
                    eventInfo.Properties["inner-error-message"] = ex.InnerException;
                    eventInfo.Properties["ForegroundColor"] = foregroundColor;
                }

                Logger.Log(eventInfo);
            }
        }
    }
}
