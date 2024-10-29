namespace DeliveryServiceLib
{
    /// <summary>
    /// Набор статусов операций.
    /// </summary>
    public enum StatusOpearation
    {
        Success,
        Error
    }

    /// <summary>
    /// Набор видов операций.
    /// </summary>
    public enum Operation
    {
        Reading,
        Writing
    }

    /// <summary>
    /// Реализует логирование основных операций.
    /// </summary>
    public class DeliveryLogger
    {
        /// <summary>
        /// Путь к файлу логирования.
        /// </summary>
        public string Path { get; private set; }    

        /// <summary>
        /// Конструктор с 1 параметром.
        /// </summary>
        /// <param name="path">Путь к файлу логирования.</param>
        public DeliveryLogger(string path)
        {
            Path = path;
        }

        /// <summary>
        /// Запись в файл логирования.
        /// </summary>
        /// <typeparam name="T">Тип объекта дополнительной информации.</typeparam>
        /// <param name="operation">Вид операции.</param>
        /// <param name="statusOperation">Статус операции.</param>
        /// <param name="obj">Объект с дополнительной информацией.</param>
        public void Log<T>(Operation operation, StatusOpearation statusOperation, T obj)
        {
            using (var sw = new StreamWriter(Path, true))
            {
                if (obj is Order order)
                {
                    sw.WriteLine($"{DateTime.Now} " +
                                 $"[{statusOperation}] " +
                                 $"{operation}" +
                                 $"\n\t{order.Id} {order.Weight} {order.CityDistrict} {order.DeliveryDateTime}");
                }
                else
                {
                    sw.WriteLine($"{DateTime.Now} " +
                                 $"[{statusOperation}] " +
                                 $"{operation} " +
                                 $"Exception " +
                                 $"{obj}");
                }
            }
        }
    }
}
