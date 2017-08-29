using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Tracing;
using log4net.Core;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.IO;
using log4net.Config;
using System.Reflection;

namespace SchemaApi.Loggings
{

    public class Log4NetTraceListener : System.Diagnostics.Tracing.EventListener
    {

        public Log4NetTraceListener() : this(typeof(Log4NetTraceListener))
        {

        }

        public Log4NetTraceListener(Type declaringType, string path = "")
        {

            this._typeLogger = declaringType;

            Initialize(declaringType, path);

            this._log = LogManager.GetLogger(this._typeLogger);
            //this._loggerEmergency = LogManager.GetLogger("LoggerEmergency");

        }

        //public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
        //{
        //    base.TraceData(eventCache, source, eventType, id, data);
        //}

        //public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        //{
        //    base.TraceData(eventCache, source, eventType, id, data);
        //}

        //public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
        //{
        //    base.TraceEvent(eventCache, source, eventType, id);
        //}

        //public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
        //{
        //    base.TraceEvent(eventCache, source, eventType, id, format, args);
        //}

        //public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
        //{
        //    base.TraceEvent(eventCache, source, eventType, id, message);
        //}

        //public override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
        //{
        //    base.TraceTransfer(eventCache, source, id, message, relatedActivityId);
        //}

        //public override void Write(string message)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(Level.Debug, message, null, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void WriteLine(string message)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(Level.Debug, message, null, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void WriteLine(object o)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(Level.Debug, string.Empty, o, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void Fail(string message)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(Level.Error, message, null, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void Fail(string message, string detailMessage)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(Level.Error, message, detailMessage, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void Write(object o)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(Level.Debug, string.Empty, o, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void Write(object o, string category)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(GetLevel(category), string.Empty, o, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void WriteLine(object o, string category)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(GetLevel(category), string.Empty, o, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void WriteLine(string message, string category)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(GetLevel(category), message, null, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        //public override void Write(string message, string category)
        //{
        //    StackFrame frame = GetMethodCallingLog();
        //    Task.Factory.StartNew(
        //        () =>
        //        {
        //            var i = CreateLogEntry(GetLevel(category), message, null, frame);
        //            this._log.Logger.Log(i);
        //        });
        //}

        #region private

        #region InitializeLog4Net

        private static void Initialize(Type declaringType, string path)
        {

            if (!_initialized)
                lock (_lock)
                    if (!_initialized)
                        ConfigureLog4Net(path);
        }

        private static ICollection ConfigureLog4Net(string path = "")
        {

            _initialized = true;

            string _path = string.Empty;
            System.Collections.IDictionary envVars = Environment.GetEnvironmentVariables();

            if (!string.IsNullOrEmpty(path) && File.Exists(path))
                _path = path;

            else if (envVars.Contains(LogFile) && File.Exists(envVars[LogFile] as string))
                _path = envVars[LogFile] as string;

            else
                _path = Path.Combine(Directory.GetCurrentDirectory(), "log4net.config");

            ICollection result = null;
            FileInfo f = new FileInfo(_path);
            if (f.Exists)
                result = XmlConfigurator.ConfigureAndWatch(f);
            else
                result = XmlConfigurator.Configure();

            return result;

        }

        #endregion

        //private static StackFrame GetMethodCallingLog()
        //{
        //    StackFrame stackFrame = null;
        //    var stackFrames = new StackTrace().GetFrames();
        //    bool t = false;
        //    for (int indexFrame = 0; indexFrame < stackFrames.Length; ++indexFrame)
        //    {
        //        stackFrame = stackFrames[indexFrame];
        //        MethodBase method = stackFrame.GetMethod();
        //        if (!t)
        //        {
        //            if (method.DeclaringType == typeof(Trace))
        //                t = true;
        //        }
        //        else
        //        {
        //            if (method.DeclaringType != typeof(Trace))
        //                break;
        //        }
        //    }

        //    return stackFrame;

        //}

        private static Level GetLevel(string category)
        {
            Level _cat = Level.Off;
            switch (category.ToLower())
            {

                case "verbose":
                    _cat = Level.Verbose;
                    break;

                case "finer":
                    _cat = Level.Finer;
                    break;

                case "trace":
                    _cat = Level.Trace;
                    break;

                case "fine":
                    _cat = Level.Fine;
                    break;

                case "debug":
                    _cat = Level.Debug;
                    break;

                case "info":
                    _cat = Level.Info;
                    break;

                case "notice":
                    _cat = Level.Notice;
                    break;

                case "finest":
                    _cat = Level.Finest;
                    break;

                case "error":
                    _cat = Level.Error;
                    break;

                case "severe":
                    _cat = Level.Severe;
                    break;

                case "critical":
                    _cat = Level.Critical;
                    break;

                case "alert":
                    _cat = Level.Alert;
                    break;

                case "fatal":
                    _cat = Level.Fatal;
                    break;

                case "emergency":
                    _cat = Level.Emergency;
                    break;

                case "log4net_debug":
                    _cat = Level.Log4Net_Debug;
                    break;

                case "warn":
                    _cat = Level.Warn;
                    break;

                default:
                    _cat = new Level(0, category);
                    break;

            }

            return _cat;

        }

        public List<KeyValuePair<string, string>> GetParameters(object parameters)
        {
            List<KeyValuePair<string, string>> _datas = new List<KeyValuePair<string, string>>();
            GetParameters(string.Empty, parameters, _datas);
            return _datas;
        }

        private void GetParameters(string name, object parameter, List<KeyValuePair<string, string>> _parameters)
        {

            if (parameter != null)
            {
                Type type = parameter.GetType();
                if (_simpleValues.Contains(type))
                {
                    if (type.IsArray)
                    {
                        StringBuilder sb = new StringBuilder();
                        SerializeArray(parameter, sb);
                        _parameters.Add(new KeyValuePair<string, string>(name, sb.ToString()));
                    }
                    else
                        _parameters.Add(new KeyValuePair<string, string>(name, parameter.ToString()));
                }
                else if (parameter is Exception)
                {
                    var exception = parameter as Exception;
                    Queue<ExceptionSerializer> lst = Bb.Sdk.Exceptions.ExceptionObjectSource.Extract(exception);
                    StringBuilder sb = new StringBuilder();
                    foreach (var item in lst)
                    {
                        sb.Append("--     Exception     --");
                        sb.AppendLine(" <BR />");

                        item.Serialize(sb);

                        sb.Append("--   End Exception   --");
                        sb.AppendLine(" <BR />");

                    }
                    _parameters.Add(new KeyValuePair<string, string>(name, sb.ToString()));
                }

                else if (type.IsClass)
                {
                    var properties = Accessors.PropertyAccessor.Get(parameter.GetType(), false);
                    foreach (var item in properties.OfType<PropertyAccessor>())
                        GetParameters(item.Name, item.GetValue(parameter), _parameters);
                }

            }

            _parameters.Add(new KeyValuePair<string, string>("processId", Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture)));

        }

        private static void SerializeArray(object parameter, StringBuilder sb)
        {
            bool t = false;
            foreach (var parame in parameter as IEnumerable)
            {

                if (t)
                    sb.Append(", ");
                else
                    t = true;

                sb.Append(parame);

            }
        }

        private log4net.Core.LoggingEvent CreateLogEntry(log4net.Core.Level level, string message, object parameters, StackFrame frame)
        {

            var _parameters = GetParameters(parameters);

            if (string.IsNullOrEmpty(message))
                message = _parameters.FirstOrDefault(c => c.Key.ToLower() == "message").Value;

            var method = frame.GetMethod();

            var result = new log4net.Core.LoggingEvent(method.ReflectedType, this._log.Logger.Repository, new LoggingEventData()
            {
                Level = level,
                Message = message,
                LoggerName = this.Name,
                TimeStamp = DateTime.UtcNow,
                LocationInfo = new LocationInfo(GetMethodName(method),
                                           method.ToString(),
                                           frame.GetFileName(),
                                           frame.GetFileLineNumber().ToString())
            }, FixFlags.All);


            foreach (var item in _parameters)
                if (!string.IsNullOrEmpty(item.Key) && item.Value != null)
                    result.Properties[item.Key] = item.Value;

            return result;

        }

        private static string GetMethodName(System.Reflection.MethodBase method)
        {
            var type = method.ReflectedType;
            if (type != null)
                return type.Name;
            else
                return method.ToString();
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            throw new NotImplementedException();
        }

        private const string LogFile = "LogFile";
        private Type _typeLogger;
        private ILog _log;
        private ILog _loggerEmergency;
        private static bool _initialized;
        private static object _lock = new object();

        private HashSet<Type> _simpleValues = new HashSet<Type>()
        {
            typeof(string),
            typeof(char),
            typeof(short), typeof(ushort), typeof(int), typeof(uint), typeof(long), typeof(ulong),typeof(float),typeof(double),
            typeof(short?), typeof(ushort?), typeof(int?), typeof(uint?), typeof(long?), typeof(ulong?),typeof(float?),typeof(double?),

            typeof(string[]),
            typeof(short[]), typeof(ushort[]), typeof(int[]), typeof(uint[]), typeof(long[]), typeof(ulong[]),typeof(float[]),typeof(double[]),
            typeof(short?[]), typeof(ushort?[]), typeof(int?[]), typeof(uint?[]), typeof(long?[]), typeof(ulong?[]),typeof(float?[]),typeof(double?[]),
        };

        #endregion private


    }


}
