CREATE TABLE IF NOT EXISTS "Profiles" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "UserId" UUID NOT NULL UNIQUE,  -- One-to-one связь с пользователем
    "Name" VARCHAR(100) NOT NULL,
    "PhotoUrl" TEXT,
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);
