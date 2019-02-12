namespace Core.Api.Models
{
    public abstract class AuthenticationHeader
    {
        public string Scheme { get; protected set; }
        public string Value { get; protected set; }

        public override string ToString()
        {
            return $"{Scheme} {Value}";
        }
    }

    public class BasicAuthenticationHeader : AuthenticationHeader
    {
        public BasicAuthenticationHeader()
        {
            Scheme = "Basic";
        }

        public BasicAuthenticationHeader(string value)
        {
            Scheme = "Basic";
            Value = value;
        }
    }

    public class BearerAuthenticationHeader : AuthenticationHeader
    {
        public BearerAuthenticationHeader()
        {
            Scheme = "Bearer";
        }

        public BearerAuthenticationHeader(string value)
        {
            Scheme = "Bearer";
            Value = value;
        }
    }
}
