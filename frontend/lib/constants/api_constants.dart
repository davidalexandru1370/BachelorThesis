abstract class ApiConstants {
  static const String BASE_URL =
      String.fromEnvironment("Environment") == "Development"
          ? "http://10.139.5.44:5000"
          : "https://api.localhost.com";
  static const String API_URL = "$BASE_URL/api";
}
