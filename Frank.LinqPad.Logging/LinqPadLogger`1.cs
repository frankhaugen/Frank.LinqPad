using Frank.Reflection;
using Microsoft.Extensions.Logging;

namespace LINQPadQuery;

public class LinqPadLogger<T>() : LinqPadLogger(typeof(T).GetFriendlyName()), ILogger<T>;