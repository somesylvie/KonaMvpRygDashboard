import sys
import csv
import boto3

dynamodb = boto3.resource('dynamodb')

tableName = 'sylvie-kona-ryg'
filename = 'C:\\Users\\sylvi\\Downloads\\rygs.csv'

# I originally imported the data into Dynamo, but then decided that sorting out AWS permissions was more hassle than I felt like for MVP

def main():
    csvfile = open(filename)

    write_to_dynamo(csv.DictReader(csvfile))

    return print("Done")

def write_to_dynamo(rows):
    table = dynamodb.Table(tableName)
    with table.batch_writer() as batch:
        for row in rows:
            batch.put_item(
                Item={
                    'Id': row['Id'],
                    'Timestamp': row['Timestamp'],
                    'Elaboration': row['Elaboration'],
                    'Emotion': row['Emotion'],
                    'MeetingHours': row['MeetingHours'],
                    'Platform': row['Platform'],
                    'PrivateElaboration': row['PrivateElaboration'],
                    'Reactions': row['Reactions'],
                    'Selection': row['Selection'],
                    'SlackMessageId': row['SlackMessageId'],
                    'SlackOrgId': row['SlackOrgId'],
                    'SlackTeamId': row['SlackTeamId'],
                    'SlackUserId': row['SlackUserId']
                }
            )

main()
