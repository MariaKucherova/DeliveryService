using System.ComponentModel.DataAnnotations;

namespace DeliveryServiceLib
{
    /// <summary>
    /// Реализует валидацию для аттрибута Вес заказа.
    /// </summary>
    public class OrderWeightAttribute : ValidationAttribute
    {
        /// <summary>
        /// Переопределение метода IsValid(object? value).
        /// </summary>
        /// <param name="value">Вес заказа.</param>
        /// <returns>
        /// Возвращает истину, если вес заказа представляет корректное значение. 
        /// Иначе возвращает ложь.
        /// </returns>
        public override bool IsValid(object? value)
        {
            if (value is double orderWeight)
            {
                if (orderWeight > 0)
                {
                    return true;
                }
                else
                {
                    ErrorMessage = "Вес заказа должен быть больше 0.";
                }
            }
            return false;
        }
    }
}
