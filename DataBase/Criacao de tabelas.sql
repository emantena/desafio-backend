BEGIN;
CREATE TABLE IF NOT EXISTS public."CnhType"
(
    "CnhTypeId" SERIAL PRIMARY KEY NOT NULL,
    "Type" character(2) COLLATE pg_catalog."default" NOT NULL,
    "Description" character varying(45) COLLATE pg_catalog."default" NOT NULL
)


CREATE TABLE IF NOT EXISTS public."DeliveryMan"
(
    "DeliveryManId" SERIAL PRIMARY KEY NOT NULL,
    "CnhTypeId" integer NOT NULL,
    "Name" character varying(75) COLLATE pg_catalog."default" NOT NULL,
    "BirthDate" date NOT NULL,
    "CNH" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    "CNPJ" character varying(14) COLLATE pg_catalog."default" NOT NULL,
    "CnhImage" character varying(256) COLLATE pg_catalog."default",
    "UserId" integer
);


CREATE TABLE IF NOT EXISTS public."Order"
(
    "OrderId" SERIAL NOT NULL,
    "UserId" integer NOT NULL,
    "CreateAt" timestamp without time zone NOT NULL,
    "RacePrice" numeric NOT NULL,
    "OrderStatusId" numeric NOT NULL,
    "DeliveryManId" integer,
    CONSTRAINT "Order_pkey" PRIMARY KEY ("OrderId")
);


CREATE TABLE IF NOT EXISTS public."OrderStatus"
(
    "OrderStatusId" numeric NOT NULL,
    "Name" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "OrderStatus_pkey" PRIMARY KEY ("OrderStatusId")
);


CREATE TABLE IF NOT EXISTS public."PlanVersion"
(
    "PlanVersionId" SERIAL NOT NULL,
    "PlanId" integer NOT NULL,
    "Price" numeric NOT NULL,
    "Active" boolean NOT NULL DEFAULT true,
    "MinDayRent" numeric,
    "AdditionalCharge" numeric NOT NULL DEFAULT 0,
    "DailyLateFee" numeric NOT NULL DEFAULT 50,
    CONSTRAINT planversion_pkey PRIMARY KEY ("PlanVersionId")
);


CREATE TABLE IF NOT EXISTS public."Plans"
(
    "PlanId" SERIAL NOT NULL ,
    "Name" character varying(45) COLLATE pg_catalog."default" NOT NULL,
    "Description" character varying(500) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT plans_pkey PRIMARY KEY ("PlanId")
);


CREATE TABLE IF NOT EXISTS public."Role"
(
    "RoleId" integer NOT NULL,
    "Name" character varying(20) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Role_pkey" PRIMARY KEY ("RoleId")
);


CREATE TABLE IF NOT EXISTS public."User"
(
    "UserId" Serial NOT NULL,
    "Name" character varying(45) COLLATE pg_catalog."default" NOT NULL,
    "Email" character varying(45) COLLATE pg_catalog."default" NOT NULL,
    "Password" character varying(500) COLLATE pg_catalog."default" NOT NULL,
    "Active" boolean NOT NULL DEFAULT true,
    "CreateAt" timestamp without time zone NOT NULL,
    "RoleId" integer NOT NULL DEFAULT 1,
    CONSTRAINT "User_pkey" PRIMARY KEY ("UserId")
);


CREATE TABLE IF NOT EXISTS public."Vehicle"
(
    "VehicleId" SERIAL NOT NULL,
    "VehicleModelId" integer NOT NULL,
    "Plate" character varying(10) COLLATE pg_catalog."default" NOT NULL,
    "YearManufacture" integer NOT NULL,
    "Active" boolean NOT NULL DEFAULT true,
    "CreateAt" timestamp without time zone NOT NULL,
    "IsRent" boolean NOT NULL DEFAULT false,
    CONSTRAINT vehicle_pkey PRIMARY KEY ("VehicleId")
);


CREATE TABLE IF NOT EXISTS public."VehicleBrand"
(
    "VehicleBrandId" INTEGER NOT NULL,
    "Brand" character varying(45) COLLATE pg_catalog."default" NOT NULL,
    "Active" boolean NOT NULL,
    CONSTRAINT vehiclebrand_pkey PRIMARY KEY ("VehicleBrandId")
);


CREATE TABLE IF NOT EXISTS public."VehicleModel"
(
    "VehicleModelId" integer NOT NULL,
    "VehicleBrandId" integer NOT NULL,
    "Model" character varying(45) COLLATE pg_catalog."default" NOT NULL,
    "Active" boolean NOT NULL,
    CONSTRAINT vehiclemodel_pkey PRIMARY KEY ("VehicleModelId")
);


CREATE TABLE IF NOT EXISTS public."VehicleRent"
(
    "VehicleRentId" SERIAL NOT NULL,
    "VehicleId" integer NOT NULL,
    "PlanVersionId" integer NOT NULL,
    "DeliverymanId" integer NOT NULL,
    "StartRent" timestamp without time zone NOT NULL,
    "PrevisionEndRent" timestamp without time zone NOT NULL,
    "EndRent" timestamp without time zone,
    CONSTRAINT vehiclerent_pkey PRIMARY KEY ("VehicleRentId")
);

ALTER TABLE IF EXISTS public."DeliveryMan"
    ADD CONSTRAINT fk_deliveryman_cnhtype1 FOREIGN KEY ("CnhTypeId")
    REFERENCES public."CnhType" ("CnhTypeId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."Order"
    ADD CONSTRAINT "fk_order_orderStatus" FOREIGN KEY ("OrderStatusId")
    REFERENCES public."OrderStatus" ("OrderStatusId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;


ALTER TABLE IF EXISTS public."Order"
    ADD CONSTRAINT fk_order_user1 FOREIGN KEY ("UserId")
    REFERENCES public."User" ("UserId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."PlanVersion"
    ADD CONSTRAINT fk_planversion_plans1 FOREIGN KEY ("PlanId")
    REFERENCES public."Plans" ("PlanId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."User"
    ADD CONSTRAINT "FK_Role" FOREIGN KEY ("RoleId")
    REFERENCES public."Role" ("RoleId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;


ALTER TABLE IF EXISTS public."Vehicle"
    ADD CONSTRAINT fk_vehicle_vehiclemodel FOREIGN KEY ("VehicleModelId")
    REFERENCES public."VehicleModel" ("VehicleModelId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."VehicleModel"
    ADD CONSTRAINT fk_vehiclemodel_vehiclebrand1 FOREIGN KEY ("VehicleBrandId")
    REFERENCES public."VehicleBrand" ("VehicleBrandId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."VehicleRent"
    ADD CONSTRAINT fk_vehiclerent_deliveryman1 FOREIGN KEY ("DeliverymanId")
    REFERENCES public."DeliveryMan" ("DeliveryManId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."VehicleRent"
    ADD CONSTRAINT fk_vehiclerent_planversion1 FOREIGN KEY ("PlanVersionId")
    REFERENCES public."PlanVersion" ("PlanVersionId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;


ALTER TABLE IF EXISTS public."VehicleRent"
    ADD CONSTRAINT fk_vehiclerent_vehicle1 FOREIGN KEY ("VehicleId")
    REFERENCES public."Vehicle" ("VehicleId") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION;

END;