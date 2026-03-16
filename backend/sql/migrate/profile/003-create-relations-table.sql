CREATE TABLE IF NOT EXISTS "Relations" (
    "Id" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    "SenderId" UUID NOT NULL,
    "ReceiverId" UUID NOT NULL,
    "Status" tRelationshipStatus NOT NULL DEFAULT 'Pending',
    "CreatedAt" TIMESTAMP NOT NULL DEFAULT NOW(),
    "UpdatedAt" TIMESTAMP NOT NULL DEFAULT NOW()
);