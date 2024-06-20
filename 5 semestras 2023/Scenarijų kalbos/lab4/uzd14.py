import csv
import sys
import requests
from zipfile import ZipFile
from io import BytesIO
from io import TextIOWrapper

def read_csv(file_path):
    domain_ratings = {}
    with open(file_path, 'r', encoding='utf-8') as file:
        csv_reader = csv.reader(file)
        for row in csv_reader:
            domain, rating = row[1], row[0]
            domain_ratings[domain] = rating
    return domain_ratings


def main():
    if len(sys.argv) != 2:
        print("Usage: python uzd14.py <domain>")
        sys.exit(1)

    domain_to_search = sys.argv[1]

    rows = {}

    filename = requests.get("http://s3-us-west-1.amazonaws.com/umbrella-static/top-1m.csv.zip").content
    zf = ZipFile(BytesIO(filename), 'r')
    for item in zf.namelist():  # archyve gali būti ne vienas failas
        file = zf.open(item)
        csvreader = csv.reader(TextIOWrapper(file, 'utf-8'))
        for row in csvreader:
            domain, raiting = row[1], row[0]
            rows[domain] = raiting

    # Read Tranco Top 1 mln CSV file
    tranco_file_path = 'C:\\Users\\Lenovo\\PycharmProjects\\top-1mtranco.csv'  # Replace with the actual file path
    tranco_ratings = read_csv(tranco_file_path)

    # Read Umbrella Top 1 mln CSV file
    #umbrella_file_path = 'C:\\Users\\Lenovo\\PycharmProjects\\top-1m.csv'  # Replace with the actual file path
    umbrella_ratings = rows.get(domain_to_search, 'Not Found')


    # Search for the domain in both dictionaries
    tranco_rating = tranco_ratings.get(domain_to_search, 'Not Found')
    #umbrella_rating = umbrella_ratings.get(domain_to_search, 'Not Found')


    # Display the results
    #print(f"Tranco rating for {domain_to_search}: {umbrella_ratings_FromWeb}")
    print(f"Tranco rating for {domain_to_search}: {tranco_rating}")
    print(f"Umbrella rating for {domain_to_search}: {umbrella_ratings}")


if __name__ == "__main__":
    main()
