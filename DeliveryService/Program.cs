using DeliveryServiceLib;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace DeliveryService
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var _deliveryData = String.Empty;
            while (_deliveryData == String.Empty || _deliveryData == null)
            {
                Console.Clear();
                Console.Write("Путь к файлу с входными данными: ");
                _deliveryData = Console.ReadLine();
            }
  
            var _deliveryLog = String.Empty;
            while (_deliveryLog == String.Empty || _deliveryLog == null)
            {
                Console.Clear();
                Console.Write("Путь к файлу логирования: ");
                _deliveryLog = Console.ReadLine();
            }
            var logger = new DeliveryLogger(_deliveryLog);

            var _deliveryOrder = String.Empty;
            while (_deliveryOrder == String.Empty || _deliveryOrder == null)
            {
                Console.Clear();
                Console.Write("Путь к файлу с результатом: ");
                _deliveryOrder = Console.ReadLine();
            }

            Operation operation = Operation.Reading;

            try
            {
                var deliveryList = new List<Order>();

                using (StreamReader reader = new StreamReader(_deliveryData))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var valueOrder = line.Split(' ');
                        if (valueOrder.Length == 5)
                        {
                            var id = new Guid(valueOrder[0]);
                            var weight = double.Parse(valueOrder[1]);
                            var cityDistrict = valueOrder[2];
                            var deliveryDateTime = DateTime.Parse($"{valueOrder[3]} {valueOrder[4]}");
                            var order = new Order(id, weight, cityDistrict, deliveryDateTime);

                            var context = new ValidationContext(order);
                            var results = new List<ValidationResult>();
                            if (!Validator.TryValidateObject(order, context, results, true))
                            {
                                var errors = "";

                                foreach (var error in results)
                                {
                                    errors += error.ErrorMessage + " ";
                                }
                                throw new Exception(errors);
                            }
                            else
                            {
                                deliveryList.Add(order);
                                operation = Operation.Reading;
                                logger.Log(operation, StatusOpearation.Success, order);
                            }
                        }
                        else
                        {
                            throw new Exception("Некорректные исходные данные.");
                        }
                    }
                }

                Console.Clear();
                Console.Write("Район доставки: ");
                var _cityDistrict = Console.ReadLine();
                if (_cityDistrict == String.Empty || _cityDistrict == null)
                {
                    _cityDistrict = ConfigurationManager.AppSettings.Get("_cityDisctrict");
                }
                
                Console.Write("Время первой доставки: ");
                var _firstDeliveryDate = Console.ReadLine();
                if (_firstDeliveryDate == String.Empty || _firstDeliveryDate == null)
                {
                    _firstDeliveryDate = ConfigurationManager.AppSettings.Get("_firstDeliveryDate");
                }
                var _firstDeliveryDateTime = DateTime.Parse(_firstDeliveryDate);

                var deliveryListFiltered = deliveryList
                                           .Where(x => x.CityDistrict == _cityDistrict 
                                                  && (x.DeliveryDateTime - _firstDeliveryDateTime).TotalMinutes < 30)
                                           .OrderBy(order => order.DeliveryDateTime);

                using (StreamWriter writer = new StreamWriter(_deliveryOrder, false))
                {
                    foreach (var order in deliveryListFiltered)
                    {
                        writer.WriteLine(order.ToString());
                        operation = Operation.Writing;
                        logger.Log(operation, StatusOpearation.Success, order);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Log(operation, StatusOpearation.Error, ex.Message);
            }
        }
    }
}
