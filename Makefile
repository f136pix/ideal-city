.PHONY: run-dbs
run-dbs:
	echo "Running the Databases"
	kubectl apply -f ./K8S/dotnet-pgsql.yml
	kubectl apply -f ./K8S/java-pgsql.yml
	echo "Databases are running"
	
.PHONY: stop-dbs
stop-dbs:
	echo "Stopping the Databases"
	kubectl delete -f ./K8S/dotnet-pgsql.yml
	kubectl delete -f ./K8S/java-pgsql.yml
	echo "Databases are stopped"
	
.PHONY: open-node-ports
open-node-ports:
	echo "Opening Node Ports"
	kubectl apply -f ./K8S/np.yml
	echo "Node Ports are opened"
	
.PHONY: run-scrapper
run-scrapper:
	echo "Running the Scrapper"
	php ./scrapper/src/App.php
	echo "Scrapper is running"