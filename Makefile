version = v1.0.0
namespace = chaos-app

build-image:
	docker build -t iskorotkov/web-todo:$(version) -f C:\Projects\web-todo/WebTodo/Dockerfile .

push-image:
	docker push iskorotkov/web-todo:$(version)

.PHONY: deploy
deploy:
	# Enable Istio injection
	kubectl label namespace $(namespace) istio-injection=enabled --overwrite

	# Deploy services
	kubectl apply -n $(namespace) \
		-f ./deploy/kubernetes/backend.yml \
		-f ./deploy/kubernetes/nginx.yml \
		-f ./deploy/kubernetes/sqlserver.yml

	# Deploy Istio gateway
	kubectl apply -n $(namespace) -f ./deploy/kubernetes/istio-gateway.yml

	# Deploy Istio destination rules
	kubectl apply -n $(namespace) -f ./deploy/kubernetes/istio-destination-rules.yml

	# Deploy Istio virtual services
	kubectl apply -n $(namespace) -f ./deploy/kubernetes/istio-services.yml

.PHONY: undeploy
undeploy:
	# Disable Istio injection
	kubectl label namespace $(namespace) istio-injection-

	# Undeploy services
	kubectl delete -n $(namespace) \
		-f ./deploy/kubernetes/backend.yml \
		-f ./deploy/kubernetes/nginx.yml \
		-f ./deploy/kubernetes/sqlserver.yml

	# Undeploy Istio gateway
	kubectl delete -n $(namespace) -f ./deploy/kubernetes/istio-gateway.yml

	# Undeploy Istio destination rules
	kubectl delete -n $(namespace) -f ./deploy/kubernetes/istio-destination-rules.yml

	# Undeploy Istio virtual services
	kubectl delete -n $(namespace) -f ./deploy/kubernetes/istio-services.yml
