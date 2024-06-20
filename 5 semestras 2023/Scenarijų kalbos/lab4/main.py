import requests
import json
import sys

def get_weather_data(location, api_key):
    url = f"http://api.weatherapi.com/v1/current.json?key={api_key}&q={location}"

    try:
        response = requests.get(url)
        response.raise_for_status()  # Check for HTTP errors

        data = response.json()
        return data
    except requests.exceptions.RequestException as e:
        print(f"Error: {e}")
        sys.exit(1)

def main():
    if len(sys.argv) != 2:
        print("Usage: python uzd13.py <location>")
        sys.exit(1)

    location = sys.argv[1]
    api_key = "2d351b25a5ec46fab0694510221411"

    weather_data = get_weather_data(location, api_key)

    temperature = weather_data['current']['temp_c']
    last_updated = weather_data['current']['last_updated']

    print(f"Temperature in {location}: {temperature}°C")
    print(f"Last updated: {last_updated}")

if __name__ == "__main__":
    main()