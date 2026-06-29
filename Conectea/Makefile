API_PROJECT=src/Conectea.API
INFRA_PROJECT=src/Conectea.Infrastructure

.PHONY: run build test migration update-db secrets-list secrets-set

run:
	dotnet run --project $(API_PROJECT)

build:
	dotnet build

test:
	dotnet test

migration:
	dotnet ef migrations add $(NAME) \
		--project $(INFRA_PROJECT) \
		--startup-project $(API_PROJECT) \
		--output-dir Persistence/Migrations

update-db:
	dotnet ef database update \
		--project $(INFRA_PROJECT) \
		--startup-project $(API_PROJECT)

secrets-list:
	cd $(API_PROJECT) && dotnet user-secrets list

secrets-set:
	cd $(API_PROJECT) && dotnet user-secrets set "$(KEY)" "$(VALUE)"
