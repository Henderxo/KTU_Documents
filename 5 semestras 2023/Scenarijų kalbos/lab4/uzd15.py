import csv
import ipaddress
import sys


def read_csv(file_path):
    with open(file_path, 'r', encoding='utf-8') as file:
        csvreader = csv.reader(file)
        data = [row for row in csvreader]
    return data


def find_country(ip_address, data):
    ip_int = int(ipaddress.IPv4Address(ip_address))

    for row in data:
        start_ip, end_ip, country_code = row
        start_ip_int = int(ipaddress.IPv4Address(start_ip))
        end_ip_int = int(ipaddress.IPv4Address(end_ip))

        if start_ip_int <= ip_int <= end_ip_int:
            return country_code

    return None


def main():
    if len(sys.argv) != 2:
        print("Usage: python uzd15.py <IPv4_address>")
        sys.exit(1)

    ipv4_address = sys.argv[1]
    data_file_path = 'C:\\Users\\Lenovo\\PycharmProjects\\dbip-country-lite-2023-12.csv'

    try:
        ip_country_data = read_csv(data_file_path)
    except FileNotFoundError:
        print(f"Error: File '{data_file_path}' not found.")
        sys.exit(1)

    country = find_country(ipv4_address, ip_country_data)

    if country:
        print(f"The country for IP address {ipv4_address} is {country}.")
    else:
        print(f"Country information not found for IP address {ipv4_address}.")


if __name__ == "__main__":
    main()
