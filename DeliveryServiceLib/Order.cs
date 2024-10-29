using System.ComponentModel.DataAnnotations;

namespace DeliveryServiceLib
{
    /// <summary>
    /// Реализует сведения о заказе.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Номер заказа.
        /// </summary>
        [Required]
        [RegularExpression(@"^([0-9A-Fa-f]{8}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{4}[-]?[0-9A-Fa-f]{12})$")]
        public Guid Id { get; private set; }

        /// <summary>
        /// Вес заказа в килограммах.
        /// </summary>
        [Required]
        [OrderWeight]
        public double Weight { get; private set; }

        /// <summary>
        /// Район заказа.
        /// </summary>
        [Required]
        public string CityDistrict { get; private set; }

        /// <summary>
        /// Время доставки заказа в формате: yyyy-MM-dd HH:mm:ss.
        /// </summary>
        [Required]
        public DateTime DeliveryDateTime { get; private set; }

        /// <summary>
        /// Конструктор с 4 параметрами.
        /// </summary>
        /// <param name="id">Номер заказа.</param>
        /// <param name="weight">Вес заказа.</param>
        /// <param name="cityDistrict">Район заказа.</param>
        /// <param name="deliveryDateTime">Вреям доставки заказа.</param>
        public Order(Guid id, double weight, string cityDistrict, DateTime deliveryDateTime)
        {
            Id = id;
            Weight = weight;
            CityDistrict = cityDistrict;
            DeliveryDateTime = deliveryDateTime;
        }

        /// <summary>
        /// Переопределение метода ToString().
        /// </summary>
        /// <returns>Возвращает строку в формате: Номер Вес Район Время доставки</returns>
        public override string ToString()
        {
            return $"{Id} {Weight} {CityDistrict} {DeliveryDateTime}";
        }
    }
}
