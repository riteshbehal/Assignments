import json

print('Loading function')

def lambda_handler(event, context):
	print('------------------------')
	print(event)
	#1. Iterate over each record
	try:
		for record in event['Records']:
			#2. Handle event by type
			if record['eventName'] == 'INSERT':
				handle_insert(record)
			elif record['eventName'] == 'MODIFY':
				handle_modify(record)
			elif record['eventName'] == 'REMOVE':
				handle_remove(record)
		print('------------------------')
		return "Success!"
	except Exception as e: 
		print(e)
		print('------------------------')
		return "Error"


def handle_insert(record):
	print("Handling INSERT Event")
	
	#3a. Get newImage content
	newImage = record['dynamodb']['NewImage']
	
	#3b. Parse values
	newPlayerId = newImage['playerId']['S']

	#3c. Print it
	print ('New row added with playerId=' + newPlayerId)

	print("Done handling INSERT Event")

def handle_modify(record):
	print("Handling MODIFY Event")

	#3a. Parse oldImage and score
	oldImage = record['dynamodb']['OldImage']
	oldScore = oldImage['score']['N']
	
	#3b. Parse oldImage and score
	newImage = record['dynamodb']['NewImage']
	newScore = newImage['score']['N']

	#3c. Check for change
	if oldScore != newScore:
		print('Scores changed - oldScore=' + str(oldScore) + ', newScore=' + str(newScore))

	print("Done handling MODIFY Event")

def handle_remove(record):
	print("Handling REMOVE Event")

	#3a. Parse oldImage
	oldImage = record['dynamodb']['OldImage']
	
	#3b. Parse values
	oldPlayerId = oldImage['playerId']['S']

	#3c. Print it
	print ('Row removed with playerId=' + oldPlayerId)

	print("Done handling REMOVE Event")