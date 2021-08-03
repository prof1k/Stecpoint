namespace SIS.First.FluentValidators
{
    using FluentValidation;
    using FluentValidation.Validators;

    using PhoneNumbers;

    /// <summary>
    /// Расширения для телефонного валидатора.
    /// </summary>
    public static class PhoneValidatorExtension
    {
        /// <summary>
        /// Поле должно соответствовать номеру телефона заданного региона.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="region"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, string> PhoneMustMatchRegion<T>(this IRuleBuilder<T, string> ruleBuilder, string region)
        {
            return ruleBuilder.SetValidator(new PhoneRegionValidator<T>(region));
        }
    }

    public class PhoneRegionValidator<T> : PropertyValidator<T, string>
    {
        private readonly string _region;

        public PhoneRegionValidator(string region)
        {
            _region = region;
        }

        public override bool IsValid(ValidationContext<T> context, string value)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();

            try
            {
                var phoneNumber = phoneNumberUtil.Parse(value, _region);
                if (!phoneNumberUtil.IsValidNumberForRegion(phoneNumber, _region))
                {
                    context.MessageFormatter.AppendArgument("PhoneRegion", _region);
                    return false;
                }
            }
            catch (NumberParseException numberParseException)
            {
                context.MessageFormatter.AppendArgument("PhoneRegion", _region);
                context.AddFailure("Номер телефона задан неверно", numberParseException.Message);
                return false;
            }

            return true;
        }

        public override string Name => nameof(PhoneRegionValidator<T>);

        protected override string GetDefaultMessageTemplate(string errorCode)
            => "{PropertyName} должен соответствовать региону {PhoneRegion}.";
    }
}