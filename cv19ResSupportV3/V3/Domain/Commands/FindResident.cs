namespace cv19ResSupportV3.V3.Domain.Commands
{
    public class FindResident
    {
        /// <summary>
        /// NHS number (10 digit number)
        /// </summary>
        /// <example>4857773456</example>
        public string NhsNumber { get; set; }
        /// <summary>
        /// Uprn
        /// </summary>
        /// <example>111111111111</example>
        public string Uprn { get; set; }
        /// <summary>
        /// First name of the resident
        /// </summary>
        /// <example>Wednesday</example>
        public string FirstName { get; set; }
        /// <summary>
        /// Last name of the resident
        /// </summary>
        /// <example>Adams</example>
        public string LastName { get; set; }
        /// <summary>
        /// Date of birth month of the resident
        /// </summary>
        /// <example>01</example>
        public string DobMonth { get; set; }
        /// <summary>
        /// Date of birth year of the resident
        /// </summary>
        /// <example>1990</example>
        public string DobYear { get; set; }
        /// <summary>
        /// Date of birth month of the resident
        /// </summary>
        /// <example>01</example>
        public string DobDay { get; set; }
        /// <summary>
        /// Postcode of the resident
        /// </summary>
        /// <example>A1 2BC</example>
        public string Postcode { get; set; }
    }
}
