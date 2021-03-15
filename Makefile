version = v1.0.0
namespace = chaos-app

build-image:
	docker build -t iskorotkov/web-todo:$(version) -f C:\Projects\web-todo/WebTodo/Dockerfile .

push-image:
	docker push iskorotkov/web-todo:$(version)

.PHONY: deploy
deploy:
	kubectl apply -n $(namespace) \
		-f ./deploy/kubernetes/backend.yml \
		-f ./deploy/kubernetes/nginx.yml \
		-f ./deploy/kubernetes/sqlserver.yml
