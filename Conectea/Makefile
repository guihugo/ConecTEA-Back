PROJECT=src/Conectea.Infrastructure
STARTUP=src/Conectea.API

migration:
	dotnet ef migrations add $(NAME) \
	--project $(PROJECT) \
	--startup-project $(STARTUP) \
	--output-dir Persistence/Migrations

update-db:
	dotnet ef database update \
	--project $(PROJECT) \
	--startup-project $(STARTUP)

remove-migration:
	dotnet ef migrations remove \
	--project $(PROJECT) \
	--startup-project $(STARTUP)

list-migrations:
	dotnet ef migrations list \
	--project $(PROJECT) \
	--startup-project $(STARTUP)