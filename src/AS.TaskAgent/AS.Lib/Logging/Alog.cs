using System;
using NLog;

namespace AS.Lib.Logging
{
    public class Alog
    {
        private Logger logger = LogManager.CreateNullLogger();
        private string loggerName = String.Empty;
        
        public Logger Logger
        {
            get
            {
                return this.logger;
            }
        }

        public Alog(string newLoggerName)
        {
            loggerName = newLoggerName;
            logger = LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Gets a value indicating whether the Trace level is enabled.
        /// </summary>
        /// <value></value>
        public bool IsTraceEnabled
        {
            get { return this.logger.IsTraceEnabled; }
        }

        /// <summary>
        /// Gets a value indicating whether the Debug level is enabled.
        /// </summary>
        /// <value></value>
        public bool IsDebugEnabled
        {
            get { return this.logger.IsDebugEnabled; }
        }

        /// <summary>
        /// Gets a value indicating whether the Info level is enabled.
        /// </summary>
        /// <value></value>
        public bool IsInfoEnabled
        {
            get { return this.logger.IsInfoEnabled; }
        }

        /// <summary>
        /// Gets a value indicating whether the Warn level is enabled.
        /// </summary>
        /// <value></value>
        public bool IsWarnEnabled
        {
            get { return this.logger.IsWarnEnabled; }
        }

        /// <summary>
        /// Gets a value indicating whether the Error level is enabled.
        /// </summary>
        /// <value></value>
        public bool IsErrorEnabled
        {
            get { return this.logger.IsErrorEnabled; }
        }

        /// <summary>
        /// Gets a value indicating whether the Fatal level is enabled.
        /// </summary>
        /// <value></value>
        public bool IsFatalEnabled
        {
            get { return this.logger.IsFatalEnabled; }
        }

        /// <summary>
        /// Gets or sets the logger name.
        /// </summary>
        /// <value></value>
        public string LoggerName
        {
            get
            {
                return this.loggerName;
            }

            set
            {
                this.loggerName = value;
                this.logger = LogManager.GetLogger(value);
            }
        }

        /// <summary>
        /// Writes the diagnostic message at the specified level.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <param name="message">A <see langword="string"/> to be written.</param>
        public void Log(string level, string message)
        {
            this.logger.Log(LogLevel.FromString(level), message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Trace level.
        /// </summary>
        /// <param name="message">A <see langword="string"/> to be written.</param>
        public void Trace(string message)
        {
            this.logger.Trace(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Debug level.
        /// </summary>
        /// <param name="message">A <see langword="string"/> to be written.</param>
        public void Debug(string message)
        {
            this.logger.Debug(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Info level.
        /// </summary>
        /// <param name="message">A <see langword="string"/> to be written.</param>
        public void Info(string message)
        {
            this.logger.Info(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Warn level.
        /// </summary>
        /// <param name="message">A <see langword="string"/> to be written.</param>
        public void Warn(string message)
        {
            this.logger.Warn(message);
        }

        /// <summary>
        /// Writes the diagnostic message at the Error level.
        /// </summary>
        /// <param name="message">A <see langword="string"/> to be written.</param>
        public void Error(string message)
        {
            this.logger.Error(message);
        }

        public void ErrorException(string message, Exception exception)
        {
            this.logger.ErrorException(message, exception);
        }




        /// <summary>
        /// Writes the diagnostic message at the Fatal level.
        /// </summary>
        /// <param name="message">A <see langword="string"/> to be written.</param>
        public void Fatal(string message)
        {
            this.logger.Fatal(message);
        }

        /// <summary>
        /// Checks if the specified log level is enabled.
        /// </summary>
        /// <param name="level">The log level.</param>
        /// <returns>
        /// A value indicating whether the specified log level is enabled.
        /// </returns>
        public bool IsEnabled(string level)
        {
            return this.logger.IsEnabled(LogLevel.FromString(level));
        }
    }
}
