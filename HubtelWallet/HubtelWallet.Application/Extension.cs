using FluentResults;
using PhoneNumbers;
using Local = HubtelWallet.Domain.Shared;

namespace HubtelWallet.Application
{
    public static class Extension
    {
        public static Local.Result<T> ToResultDto<T>(this Result<T> result)
        {
            if (result.IsSuccess)
                return new Local.Result<T>(true, result.Successes.FirstOrDefault()?.Message, Enumerable.Empty<Local.Error>(), result.ValueOrDefault);

            return new Local.Result<T>(false, result.Reasons.FirstOrDefault()?.Message, TransformErrors(result.Errors), result.ValueOrDefault);
        }

        public static DateOnly ToDateOnly(this DateTime date)
        {
            return DateOnly.FromDateTime(date);
        }

        public static string ToInternationalNumber(this string number, string region = "GH")
        {
            PhoneNumberUtil _formatter = PhoneNumberUtil.GetInstance();
            try
            {
                PhoneNumber phoneNumber = _formatter.Parse(number, region);

                string formattedPhoneNumber;
                if (_formatter.IsValidNumber(phoneNumber))
                    formattedPhoneNumber = _formatter.Format(phoneNumber, PhoneNumberFormat.E164);
                else
                    formattedPhoneNumber = $"+{phoneNumber.CountryCode}{phoneNumber.NationalNumber}";

                return formattedPhoneNumber[1..]; //exclude the plus sign
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        #region utils

        private static IEnumerable<Local.Error> TransformErrors(List<IError> errors)
        {
            return errors.Select(TransformError);
        }

        private static Local.Error TransformError(IError error)
        {
            var errorCode = TransformErrorCode(error);

            return new Local.Error(error.Message, errorCode);
        }

        private static string TransformErrorCode(IError error)
        {
            if (error.Metadata.TryGetValue("ErrorCode", out var errorCode))
                return errorCode as string;

            return "";
        }

        #endregion utils
    }
}
