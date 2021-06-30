namespace Framework.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agent",
                c => new
                    {
                        agent_id = c.Int(nullable: false, identity: true),
                        city_id = c.Int(nullable: false),
                        name = c.String(maxLength: 500, unicode: false),
                        companyName = c.String(maxLength: 800, unicode: false),
                        address = c.String(maxLength: 400, unicode: false),
                        postalZipCode = c.String(maxLength: 20, unicode: false),
                        website = c.String(maxLength: 250, unicode: false),
                        userName = c.String(maxLength: 150, unicode: false),
                        password = c.String(maxLength: 10, unicode: false),
                        forwardedTo = c.String(maxLength: 250, unicode: false),
                        demarcation_no = c.String(maxLength: 20, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        dateProgramInformation = c.DateTime(),
                        programInformationType = c.String(maxLength: 250, unicode: false),
                        programInformationNotes = c.String(maxLength: 2000, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.agent_id)
                .ForeignKey("dbo.City", t => t.city_id)
                .Index(t => t.city_id);
            
            CreateTable(
                "dbo.AgentCommissions",
                c => new
                    {
                        agentCommissions_id = c.Int(nullable: false, identity: true),
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        agent_id = c.Int(nullable: false),
                        currency = c.String(maxLength: 5, unicode: false),
                        projectInvoice = c.String(maxLength: 50, unicode: false),
                        quotedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        discountGiven = c.Decimal(nullable: false, precision: 18, scale: 2),
                        nextDepotAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        partsPurchased = c.String(maxLength: 250, unicode: false),
                        timeNeeded = c.String(maxLength: 50, unicode: false),
                        amountComm = c.Decimal(nullable: false, precision: 18, scale: 2),
                        commisionPaid = c.Decimal(nullable: false, precision: 18, scale: 2),
                        commisionGiven = c.Decimal(nullable: false, precision: 18, scale: 2),
                        received = c.DateTime(),
                        quoted = c.DateTime(),
                        goAhead = c.DateTime(),
                        customerPaid = c.DateTime(),
                        id_old = c.Int(),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.agentCommissions_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .ForeignKey("dbo.Agent", t => t.agent_id)
                .Index(t => t.serviceOrder_id)
                .Index(t => t.agent_id);
            
            CreateTable(
                "dbo.ServiceOrder",
                c => new
                    {
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0, identity: true),
                        date = c.DateTime(nullable: false),
                        user_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        customer_id = c.Int(),
                        customerContact_id = c.Int(),
                        customerContactName = c.String(maxLength: 200, unicode: false),
                        customerContactEmail = c.String(maxLength: 150, unicode: false),
                        customerContactTelephone = c.String(maxLength: 20, unicode: false),
                        customerContactMobile = c.String(maxLength: 20, unicode: false),
                        referredBy = c.String(maxLength: 250, unicode: false),
                        dateAssigned = c.DateTime(),
                        userAssigned_id = c.String(maxLength: 128, unicode: false),
                        status = c.String(maxLength: 150, unicode: false),
                        extensionStatus = c.String(maxLength: 500, unicode: false),
                        takenBy = c.String(maxLength: 250, unicode: false),
                        CSR = c.String(maxLength: 250, unicode: false),
                        typeOfService = c.String(maxLength: 200, unicode: false),
                        serviceToExecute = c.String(maxLength: 2000, unicode: false),
                        mostImportantFilesToRecovery = c.String(maxLength: 2000, unicode: false),
                        estimate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        locationReceived_id = c.Int(nullable: false),
                        location_id = c.Int(),
                        arrivedBy = c.String(maxLength: 250, unicode: false),
                        wayBillNumber = c.String(maxLength: 100, unicode: false),
                        packageCondidition = c.String(maxLength: 250, unicode: false),
                        smartNumber = c.String(maxLength: 100, unicode: false),
                        techsName = c.String(maxLength: 250, unicode: false),
                        id_old = c.Int(),
                        note = c.String(maxLength: 5000, unicode: false),
                        originOfServiceOrder = c.String(maxLength: 500, unicode: false),
                        approvalDate = c.DateTime(),
                        statusDate = c.DateTime(nullable: false),
                        subStatus = c.String(maxLength: 150, unicode: false, nullable:true),
                        inTransfer = c.Boolean(nullable:true)
                    })
                .PrimaryKey(t => t.serviceOrder_id)
                .ForeignKey("dbo.Locations", t => t.location_id)
                .ForeignKey("dbo.Locations", t => t.locationReceived_id)
                .ForeignKey("dbo.Customer", t => t.customer_id)
                .ForeignKey("dbo.Usuario", t => t.user_id)
                .ForeignKey("dbo.Usuario", t => t.userAssigned_id)
                .Index(t => t.user_id)
                .Index(t => t.customer_id)
                .Index(t => t.userAssigned_id)
                .Index(t => t.locationReceived_id)
                .Index(t => t.location_id);
            
            CreateTable(
                "dbo.ServiceOrderContact",
                c => new
                    {
                        serviceOrderContact_id = c.Int(nullable: false, identity: true),
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        contact_id = c.Int(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.serviceOrderContact_id)
                .ForeignKey("dbo.Contact", t => t.contact_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id)
                .Index(t => t.contact_id);
            
            CreateTable(
                "dbo.Contact",
                c => new
                    {
                        contact_id = c.Int(nullable: false, identity: true),
                        typeContact_id = c.Int(nullable: false),
                        name = c.String(maxLength: 200, unicode: false),
                        city_id = c.Int(),
                        tollFree = c.String(maxLength: 20, unicode: false),
                        telephone = c.String(maxLength: 20, unicode: false),
                        extension = c.String(maxLength: 6, unicode: false),
                        fax = c.String(maxLength: 20, unicode: false),
                        mobile = c.String(maxLength: 20, unicode: false),
                        email = c.String(maxLength: 150, unicode: false),
                        other = c.String(maxLength: 150, unicode: false),
                        notes = c.String(maxLength: 500, unicode: false),
                        receiveNotifications = c.Boolean(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.contact_id)
                .ForeignKey("dbo.City", t => t.city_id)
                .ForeignKey("dbo.TypeOfContact", t => t.typeContact_id)
                .Index(t => t.typeContact_id)
                .Index(t => t.city_id);
            
            CreateTable(
                "dbo.AgentContacts",
                c => new
                    {
                        agentContact_id = c.Int(nullable: false, identity: true),
                        agent_id = c.Int(nullable: false),
                        contact_id = c.Int(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.agentContact_id)
                .ForeignKey("dbo.Contact", t => t.contact_id)
                .ForeignKey("dbo.Agent", t => t.agent_id)
                .Index(t => t.agent_id)
                .Index(t => t.contact_id);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        city_id = c.Int(nullable: false, identity: true),
                        state_id = c.Int(nullable: false),
                        country_id = c.Int(nullable: false),
                        name = c.String(maxLength: 500, unicode: false),
                        ddd = c.Int(nullable: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.city_id)
                .ForeignKey("dbo.Country", t => t.country_id)
                .ForeignKey("dbo.State", t => t.state_id)
                .Index(t => t.state_id)
                .Index(t => t.country_id);
            
            CreateTable(
                "dbo.Country",
                c => new
                    {
                        country_id = c.Int(nullable: false, identity: true),
                        initials = c.String(maxLength: 3, unicode: false),
                        name = c.String(maxLength: 250, unicode: false),
                        idd = c.Int(nullable: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.country_id);
            
            CreateTable(
                "dbo.State",
                c => new
                    {
                        state_id = c.Int(nullable: false, identity: true),
                        country_id = c.Int(nullable: false),
                        initials = c.String(maxLength: 6, unicode: false),
                        name = c.String(maxLength: 250, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.state_id)
                .ForeignKey("dbo.Country", t => t.country_id)
                .Index(t => t.country_id);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        location_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 200, unicode: false),
                        cnpj = c.String(maxLength: 128, unicode: false),
                        ie = c.String(maxLength: 128, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        companyName = c.String(maxLength: 400, unicode: false),
                        address = c.String(maxLength: 400, unicode: false),
                        district = c.String(maxLength: 150, unicode: false),
                        postalZipCode = c.String(maxLength: 20, unicode: false),
                        city_id = c.Int(nullable: false),
                        tollFree = c.String(maxLength: 20, unicode: false),
                        telephone = c.String(maxLength: 20, unicode: false),
                        phoneExtension = c.String(maxLength: 20, unicode: false),
                        sacContactName = c.String(maxLength: 500, unicode: false),
                        sacContactEmail = c.String(maxLength: 500, unicode: false),
                        line1 = c.String(maxLength: 20, unicode: false),
                        line2 = c.String(maxLength: 20, unicode: false),
                        line3 = c.String(maxLength: 20, unicode: false),
                        line4 = c.String(maxLength: 20, unicode: false),
                        line5 = c.String(maxLength: 20, unicode: false),
                        fax1 = c.String(maxLength: 20, unicode: false),
                        fax2 = c.String(maxLength: 20, unicode: false),
                        website = c.String(maxLength: 250, unicode: false),
                        gatewayIp = c.String(maxLength: 20, unicode: false),
                        ipRange = c.String(maxLength: 20, unicode: false),
                        primaryDNS = c.String(maxLength: 20, unicode: false),
                        secondaryDNS = c.String(maxLength: 20, unicode: false),
                        subNet = c.String(maxLength: 20, unicode: false),
                        internalGateway = c.String(maxLength: 20, unicode: false),
                        internalSubNet = c.String(maxLength: 20, unicode: false),
                        adminPcIp = c.String(maxLength: 20, unicode: false),
                        adminMachineIp = c.String(maxLength: 20, unicode: false),
                        adminMachineUser = c.String(maxLength: 20, unicode: false),
                        adminMachinePwd = c.String(maxLength: 20, unicode: false),
                        labMachineIP = c.String(maxLength: 20, unicode: false),
                        labMachineUser = c.String(maxLength: 20, unicode: false),
                        labMachinePwd = c.String(maxLength: 20, unicode: false),
                        qcAdminMachineIp = c.String(maxLength: 20, unicode: false),
                        qcAdminMachineUser = c.String(maxLength: 20, unicode: false),
                        qcAdminMachinePwd = c.String(maxLength: 20, unicode: false),
                        vncAdminMachineIp = c.String(maxLength: 20, unicode: false),
                        vncAdminMachineUser = c.String(maxLength: 20, unicode: false),
                        vncAdminMachinePwd = c.String(maxLength: 20, unicode: false),
                        vncLabMachineIP = c.String(maxLength: 20, unicode: false),
                        vncLabMachineUser = c.String(maxLength: 20, unicode: false),
                        vncLabMachinePwd = c.String(maxLength: 20, unicode: false),
                        ftpIp = c.String(maxLength: 20, unicode: false),
                        ftpUser = c.String(maxLength: 20, unicode: false),
                        ftpPwd = c.String(maxLength: 20, unicode: false),
                        newsGroupIp = c.String(maxLength: 20, unicode: false),
                        newsGroupUser = c.String(maxLength: 20, unicode: false),
                        newsGroupPwd = c.String(maxLength: 20, unicode: false),
                        otherInformations = c.String(maxLength: 2000, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        bank = c.String(maxLength: 250, unicode: false),
                        accountAgency = c.String(maxLength: 20, unicode: false),
                        accountNumber = c.String(maxLength: 20, unicode: false),
                        accountType = c.String(maxLength: 50, unicode: false),
                        maxParcels = c.String(maxLength: 4, unicode: false),
                        id_old = c.Int(),
                        OS_Series = c.String(maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.location_id)
                .ForeignKey("dbo.City", t => t.city_id)
                .Index(t => t.city_id);
            
            CreateTable(
                "dbo.Stock",
                c => new
                    {
                        stock_id = c.Int(nullable: false, identity: true),
                        media_id = c.Int(),
                        component_id = c.Int(),
                        material = c.String(nullable: false, maxLength: 2000, unicode: false),
                        dateOfMovement = c.DateTime(nullable: false),
                        quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                        typeOfMovement = c.String(nullable: false, maxLength: 128, unicode: false),
                        stockAddress = c.String(maxLength: 2000, unicode: false),
                        location_id = c.Int(),
                        serviceOrder_id = c.Decimal(precision: 18, scale: 0),
                        notes = c.String(maxLength: 8000, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        lastUpdateDate = c.DateTime(nullable: false),
                        lastUpdateUser_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.stock_id)
                .ForeignKey("dbo.Component", t => t.component_id, cascadeDelete: true)
                .ForeignKey("dbo.Media", t => t.media_id, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.location_id)
                .Index(t => t.media_id)
                .Index(t => t.component_id)
                .Index(t => t.location_id);
            
            CreateTable(
                "dbo.Component",
                c => new
                    {
                        component_id = c.Int(nullable: false, identity: true),
                        description = c.String(maxLength: 500, unicode: false),
                        color = c.String(maxLength: 10, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                        stockAddress = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.component_id);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        media_id = c.Int(nullable: false, identity: true),
                        manufacturer_id = c.Int(),
                        supplier_id = c.Int(),
                        location_id = c.Int(),
                        mediaStatus_id = c.Int(nullable: false),
                        model = c.String(maxLength: 200, unicode: false),
                        make = c.String(maxLength: 500, unicode: false),
                        serial_no = c.String(maxLength: 40, unicode: false),
                        part_no = c.String(maxLength: 40, unicode: false),
                        revision_no = c.String(maxLength: 40, unicode: false),
                        firmware_no = c.String(maxLength: 40, unicode: false),
                        size = c.String(maxLength: 40, unicode: false),
                        interfaceType = c.String(maxLength: 128, unicode: false),
                        pcb_id = c.String(maxLength: 40, unicode: false),
                        dateEntered = c.DateTime(nullable: false),
                        mlc_no = c.String(maxLength: 40, unicode: false),
                        mfgDate = c.DateTime(),
                        oem_no = c.String(maxLength: 40, unicode: false),
                        upLevel_no = c.String(maxLength: 40, unicode: false),
                        series = c.String(maxLength: 500, unicode: false),
                        condition = c.String(maxLength: 200, unicode: false),
                        conditionInformation = c.String(maxLength: 500, unicode: false),
                        dcmSite_no = c.String(maxLength: 40, unicode: false),
                        purchaseFrom = c.String(maxLength: 200, unicode: false),
                        purchaseCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        hda = c.String(maxLength: 200, unicode: false),
                        pcb = c.String(maxLength: 500, unicode: false),
                        id_old = c.Int(),
                        extensionParts = c.String(maxLength: 4000, unicode: false),
                        stockAddress = c.String(maxLength: 250, unicode: false),
                        madeIN = c.String(maxLength: 250, unicode: false),
                        modelInputByCustomer = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.media_id)
                .ForeignKey("dbo.Manufacturer", t => t.manufacturer_id)
                .ForeignKey("dbo.MediaStatus", t => t.mediaStatus_id)
                .ForeignKey("dbo.Suppliers", t => t.supplier_id)
                .ForeignKey("dbo.Locations", t => t.location_id)
                .Index(t => t.manufacturer_id)
                .Index(t => t.supplier_id)
                .Index(t => t.location_id)
                .Index(t => t.mediaStatus_id);
            
            CreateTable(
                "dbo.Manufacturer",
                c => new
                    {
                        manufacturer_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.manufacturer_id);
            
            CreateTable(
                "dbo.MediaModels",
                c => new
                    {
                        mediaModels_id = c.Int(nullable: false, identity: true),
                        manufacturer_id = c.Int(nullable: false),
                        model = c.String(maxLength: 500, unicode: false),
                        compatibility = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.mediaModels_id)
                .ForeignKey("dbo.Manufacturer", t => t.manufacturer_id)
                .Index(t => t.manufacturer_id);
            
            CreateTable(
                "dbo.MediaStatus",
                c => new
                    {
                        mediaStatus_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.mediaStatus_id);
            
            CreateTable(
                "dbo.PartNeeded",
                c => new
                    {
                        partNeeded_id = c.Int(nullable: false, identity: true),
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        partNeeded = c.String(maxLength: 500, unicode: false),
                        supplier_id = c.Int(),
                        partCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        arriving = c.DateTime(),
                        media_id = c.Int(),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.partNeeded_id)
                .ForeignKey("dbo.ServiceOrderQuoting", t => t.serviceOrder_id)
                .ForeignKey("dbo.Suppliers", t => t.supplier_id)
                .ForeignKey("dbo.Media", t => t.media_id)
                .Index(t => t.serviceOrder_id)
                .Index(t => t.supplier_id)
                .Index(t => t.media_id);
            
            CreateTable(
                "dbo.ServiceOrderQuoting",
                c => new
                    {
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        partNeeded_id = c.Int(),
                        quoteEstimate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        quotedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        discountGivem = c.Decimal(nullable: false, precision: 18, scale: 2),
                        nextDepotAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        currency = c.String(maxLength: 4, unicode: false),
                        timeNeeded = c.String(maxLength: 200, unicode: false),
                        dueDate = c.DateTime(),
                        destination = c.String(maxLength: 250, unicode: false),
                        quoteLines = c.String(maxLength: 4000, unicode: false),
                        id_old = c.Int(),
                        quotedFinished = c.Boolean(),
                        statusQuoting = c.String(maxLength: 50, unicode: false),
                        quoteDays = c.Int(),
                        quoteDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.serviceOrder_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        supplier_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        address = c.String(maxLength: 500, unicode: false),
                        postalZipCode = c.String(maxLength: 20, unicode: false),
                        city_id = c.Int(nullable: false),
                        website = c.String(maxLength: 200, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.supplier_id)
                .ForeignKey("dbo.City", t => t.city_id)
                .Index(t => t.city_id);
            
            CreateTable(
                "dbo.SuppliersContact",
                c => new
                    {
                        suppliersContact_id = c.Int(nullable: false, identity: true),
                        supplier_id = c.Int(nullable: false),
                        contact_id = c.Int(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.suppliersContact_id)
                .ForeignKey("dbo.Suppliers", t => t.supplier_id)
                .ForeignKey("dbo.Contact", t => t.contact_id)
                .Index(t => t.supplier_id)
                .Index(t => t.contact_id);
            
            CreateTable(
                "dbo.ServiceOrderMedias",
                c => new
                    {
                        serviceOrderMedias_id = c.Int(nullable: false, identity: true),
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        media_id = c.Int(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.serviceOrderMedias_id)
                .ForeignKey("dbo.Media", t => t.media_id, cascadeDelete: true)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id)
                .Index(t => t.media_id);
            
            CreateTable(
                "dbo.RoleLocations",
                c => new
                    {
                        roleLocations_id = c.Int(nullable: false, identity: true),
                        location_id = c.Int(nullable: false),
                        id_perfil = c.String(nullable: false, maxLength: 128, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.roleLocations_id)
                .ForeignKey("dbo.Perfil", t => t.id_perfil)
                .ForeignKey("dbo.Locations", t => t.location_id)
                .Index(t => t.location_id)
                .Index(t => t.id_perfil);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        id_perfil = c.String(nullable: false, maxLength: 128, unicode: false),
                        nome = c.String(nullable: false, maxLength: 256, unicode: false),
                        id_old = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id_perfil)
                .Index(t => t.nome, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Perfil_Area",
                c => new
                    {
                        id_perfil = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_area = c.Guid(nullable: false),
                        ind_visualizar = c.Boolean(nullable: false),
                        ind_cadastrar = c.Boolean(nullable: false),
                        ind_excluir = c.Boolean(nullable: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_perfil, t.id_area })
                .ForeignKey("dbo.Area", t => t.id_area)
                .ForeignKey("dbo.Perfil", t => t.id_perfil)
                .Index(t => t.id_perfil)
                .Index(t => t.id_area);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        id_area = c.Guid(nullable: false),
                        id_area_mae = c.Guid(),
                        nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        ordem = c.Int(nullable: false),
                        controller = c.String(maxLength: 50, unicode: false),
                        action = c.String(maxLength: 50, unicode: false),
                        help = c.String(maxLength: 8000, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_area)
                .ForeignKey("dbo.Area", t => t.id_area_mae)
                .Index(t => t.id_area_mae);
            
            CreateTable(
                "dbo.Usuario_Perfil",
                c => new
                    {
                        id_usuario = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_perfil = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.id_usuario, t.id_perfil })
                .ForeignKey("dbo.Usuario", t => t.id_usuario)
                .ForeignKey("dbo.Perfil", t => t.id_perfil)
                .Index(t => t.id_usuario)
                .Index(t => t.id_perfil);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        id_usuario = c.String(nullable: false, maxLength: 128, unicode: false),
                        nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        telefone = c.String(maxLength: 15, unicode: false),
                        celular = c.String(maxLength: 15, unicode: false),
                        ativo = c.Boolean(nullable: false),
                        url_file = c.String(maxLength: 8000, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                        id_old = c.Int(),
                        location_id = c.Int(nullable: false),
                        tokenCode = c.String(maxLength: 500, unicode: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 128, unicode: false),
                        SecurityStamp = c.String(maxLength: 128, unicode: false),
                        PhoneNumber = c.String(maxLength: 128, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.id_usuario)
                .ForeignKey("dbo.Locations", t => t.location_id)
                .Index(t => t.location_id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Usuario_Claim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                        ClaimType = c.String(maxLength: 128, unicode: false),
                        ClaimValue = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.LabNotes",
                c => new
                    {
                        labNote_id = c.Int(nullable: false, identity: true),
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        note = c.String(maxLength: 8000, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.labNote_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .ForeignKey("dbo.Usuario", t => t.userRegistration_id)
                .Index(t => t.serviceOrder_id)
                .Index(t => t.userRegistration_id);
            
            CreateTable(
                "dbo.Usuario_Login",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Usuario", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        note_id = c.Int(nullable: false, identity: true),
                        description = c.String(maxLength: 8000, unicode: false),
                        date = c.DateTime(nullable: false),
                        typenote_id = c.Int(nullable: false),
                        user_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.note_id)
                .ForeignKey("dbo.TypeOfNote", t => t.typenote_id)
                .ForeignKey("dbo.Usuario", t => t.user_id)
                .Index(t => t.typenote_id)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.ServiceOrderNotes",
                c => new
                    {
                        serviceOrderNotes_id = c.Int(nullable: false, identity: true),
                        note_id = c.Int(nullable: false),
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                        serviceOrderStatus = c.String(nullable: false, maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.serviceOrderNotes_id)
                .ForeignKey("dbo.Notes", t => t.note_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.note_id)
                .Index(t => t.serviceOrder_id);
            
            CreateTable(
                "dbo.TypeOfNote",
                c => new
                    {
                        typenote_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.typenote_id);
            
            CreateTable(
                "dbo.ServiceOrderBilling",
                c => new
                    {
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        billingAddress = c.String(maxLength: 500, unicode: false),
                        billingCompany = c.String(maxLength: 500, unicode: false),
                        billingCity_id = c.Int(nullable: false),
                        billingPostalZipCode = c.String(maxLength: 50, unicode: false),
                        billingDistrict = c.String(maxLength: 500, unicode: false),
                        paymentMethod = c.String(maxLength: 100, unicode: false),
                        creditCardNumber = c.String(maxLength: 20, unicode: false),
                        nameCreditCard = c.String(maxLength: 200, unicode: false),
                        expireCreditCard = c.String(maxLength: 7, unicode: false),
                        originalQuotedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        discountCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        amountToBeBilled = c.Decimal(nullable: false, precision: 18, scale: 2),
                        invoiceNumber = c.String(maxLength: 100, unicode: false),
                        invoicedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        datePaid = c.DateTime(),
                        referredBy = c.String(maxLength: 200, unicode: false),
                        commissionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        comissionDate = c.DateTime(),
                        partsNeeded = c.Int(nullable: false),
                        partsAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.serviceOrder_id)
                .ForeignKey("dbo.City", t => t.billingCity_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id)
                .Index(t => t.billingCity_id);
            
            CreateTable(
                "dbo.ServiceOrderShipping",
                c => new
                    {
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        shipCompany = c.String(maxLength: 500, unicode: false),
                        shipAddress = c.String(maxLength: 500, unicode: false),
                        shipCity_id = c.Int(nullable: false),
                        shipPostalZipCode = c.String(maxLength: 200, unicode: false),
                        shipDistrict = c.String(maxLength: 128, unicode: false),
                        shipContact = c.String(maxLength: 250, unicode: false),
                        shipTelephone = c.String(maxLength: 20, unicode: false),
                        shipEmail = c.String(maxLength: 250, unicode: false),
                        shipMethod = c.String(maxLength: 250, unicode: false),
                        shipAccountNumber = c.String(maxLength: 150, unicode: false),
                        shipTrackingNumber = c.String(maxLength: 150, unicode: false),
                        shipMediaStatus = c.String(maxLength: 150, unicode: false),
                        shipMediaDate = c.DateTime(),
                        shipDataShipped = c.DateTime(),
                        shipInstructions = c.String(maxLength: 2000, unicode: false),
                        shipContents = c.String(maxLength: 2000, unicode: false),
                        shipPreRecoveryInfo = c.String(maxLength: 2000, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.serviceOrder_id)
                .ForeignKey("dbo.City", t => t.shipCity_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id)
                .Index(t => t.shipCity_id);
            
            CreateTable(
                "dbo.CustomerContact",
                c => new
                    {
                        customerContact_id = c.Int(nullable: false, identity: true),
                        customer_id = c.Int(nullable: false),
                        contact_id = c.Int(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.customerContact_id)
                .ForeignKey("dbo.Customer", t => t.customer_id)
                .ForeignKey("dbo.Contact", t => t.contact_id)
                .Index(t => t.customer_id)
                .Index(t => t.contact_id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        customer_id = c.Int(nullable: false, identity: true),
                        email = c.String(maxLength: 250, unicode: false),
                        name = c.String(maxLength: 250, unicode: false),
                        address = c.String(maxLength: 500, unicode: false),
                        addressNumber = c.String(maxLength: 20, unicode: false),
                        addressExtension = c.String(maxLength: 150, unicode: false),
                        district = c.String(maxLength: 150, unicode: false),
                        postalZipCode = c.String(maxLength: 20, unicode: false),
                        website = c.String(maxLength: 250, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        hearOfUs = c.String(maxLength: 150, unicode: false),
                        pointOfContact = c.String(maxLength: 150, unicode: false),
                        creditTerms = c.String(maxLength: 200, unicode: false),
                        password = c.String(maxLength: 20, unicode: false),
                        percentage = c.Decimal(precision: 18, scale: 2),
                        cpfCnpj = c.String(maxLength: 20, unicode: false),
                        rgIdState = c.String(maxLength: 20, unicode: false),
                        notes = c.String(maxLength: 500, unicode: false),
                        id_old = c.Int(),
                        business_type = c.String(maxLength: 500, unicode: false),
                        tokenCode = c.String(maxLength: 255, unicode: false),
                        dateLastAccessPortal = c.DateTime(),
                        dateLastUpdateInformations = c.DateTime(),
                    })
                .PrimaryKey(t => t.customer_id);
            
            CreateTable(
                "dbo.TypeOfContact",
                c => new
                    {
                        typeContact_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.typeContact_id);
            
            CreateTable(
                "dbo.ServiceOrderEvaluation",
                c => new
                    {
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        calledAbout = c.String(maxLength: 250, unicode: false),
                        estimatedGivenFrom = c.Decimal(nullable: false, precision: 18, scale: 2),
                        estimatedGivenTo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        make = c.String(maxLength: 100, unicode: false),
                        serial_no = c.String(maxLength: 100, unicode: false),
                        makeOfComputer = c.String(maxLength: 150, unicode: false),
                        model = c.String(maxLength: 250, unicode: false),
                        interfaceOfdevice = c.String(maxLength: 250, unicode: false),
                        operatingSystem = c.String(maxLength: 250, unicode: false),
                        operatingSystemVersion = c.String(maxLength: 250, unicode: false),
                        partitionInfo = c.String(maxLength: 500, unicode: false),
                        raidType = c.String(maxLength: 250, unicode: false),
                        controlledType = c.String(maxLength: 250, unicode: false),
                        numberOfVolumes = c.Int(),
                        blockSize = c.Decimal(nullable: false, precision: 18, scale: 4),
                        raidDetails = c.String(maxLength: 2000, unicode: false),
                        numberOftapes = c.Int(),
                        typeOfbackupSystem = c.String(maxLength: 250, unicode: false),
                        numberOfSessions = c.Int(),
                        dataCompressionType = c.String(maxLength: 250, unicode: false),
                        tapeDetails = c.String(maxLength: 500, unicode: false),
                        failureMalfunction = c.String(maxLength: 2500, unicode: false),
                        preRecoveryInfo = c.String(maxLength: 2500, unicode: false),
                        criticalTargetData = c.String(maxLength: 2500, unicode: false),
                        fileAllocationType = c.String(maxLength: 250, unicode: false),
                        numberOfDrivesInSystem = c.Int(),
                        makeOfController = c.String(maxLength: 250, unicode: false),
                        faultFound = c.String(maxLength: 250, unicode: false),
                        jobClass = c.String(maxLength: 250, unicode: false),
                        techTeam = c.String(maxLength: 250, unicode: false),
                        processedWhere = c.String(maxLength: 250, unicode: false),
                        techNotes = c.String(maxLength: 4000, unicode: false),
                        id_old = c.Int(),
                        diagnosisFinished = c.Boolean(),
                    })
                .PrimaryKey(t => t.serviceOrder_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id);
            
            CreateTable(
                "dbo.ServiceOrderInquiryFollowUp",
                c => new
                    {
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(maxLength: 128, unicode: false),
                        causeNotSent = c.String(maxLength: 500, unicode: false),
                        sentSomewhereElseWhere = c.String(maxLength: 500, unicode: false),
                        comments = c.String(maxLength: 2000, unicode: false),
                        dateComplete = c.DateTime(),
                        userFollowUp_id = c.String(maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.serviceOrder_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id);
            
            CreateTable(
                "dbo.ServiceOrderRecoveryFollowUp",
                c => new
                    {
                        serviceOrder_id = c.Decimal(nullable: false, precision: 18, scale: 0),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        mediaStatus = c.String(maxLength: 100, unicode: false),
                        rateOurService = c.String(maxLength: 100, unicode: false),
                        wouldBeReference = c.String(maxLength: 100, unicode: false),
                        sendLetterReference = c.String(maxLength: 100, unicode: false),
                        dateComplete = c.DateTime(),
                        introFaxed = c.DateTime(),
                        emailSent = c.DateTime(),
                        comments = c.String(maxLength: 2000, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.serviceOrder_id)
                .ForeignKey("dbo.ServiceOrder", t => t.serviceOrder_id)
                .Index(t => t.serviceOrder_id);
            
            CreateTable(
                "dbo.BusinessType",
                c => new
                    {
                        business_type_id = c.Int(nullable: false, identity: true),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.business_type_id);
            
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        currency_id = c.Int(nullable: false, identity: true),
                        initials = c.String(maxLength: 4, unicode: false),
                        name = c.String(maxLength: 50, unicode: false),
                        id_old = c.Int(),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.currency_id);
            
            CreateTable(
                "dbo.DocumentType",
                c => new
                    {
                        documentType_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        url_file = c.String(maxLength: 8000, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.documentType_id);
            
            CreateTable(
                "dbo.FaultFound",
                c => new
                    {
                        faultFound_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.faultFound_id);
            
            CreateTable(
                "dbo.FileAlocationType",
                c => new
                    {
                        fileAlocationType_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.fileAlocationType_id);
            
            CreateTable(
                "dbo.HDAConditions",
                c => new
                    {
                        hdaCondition_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.hdaCondition_id);
            
            CreateTable(
                "dbo.HearOfUs",
                c => new
                    {
                        hearOfUs_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.hearOfUs_id);
            
            CreateTable(
                "dbo.Interfaces",
                c => new
                    {
                        interface_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.interface_id);
            
            CreateTable(
                "dbo.JobClass",
                c => new
                    {
                        jobClass_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.jobClass_id);
            
            CreateTable(
                "dbo.Make",
                c => new
                    {
                        make_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.make_id);
            
            CreateTable(
                "dbo.MediaConditions",
                c => new
                    {
                        mediaConditions_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.mediaConditions_id);
            
            CreateTable(
                "dbo.OpSystem",
                c => new
                    {
                        opSystem_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.opSystem_id);
            
            CreateTable(
                "dbo.PackageConditions",
                c => new
                    {
                        packageConditions_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.packageConditions_id);
            
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        paymentMethods_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        id_old = c.Int(),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.paymentMethods_id);
            
            CreateTable(
                "dbo.PCBConditions",
                c => new
                    {
                        pcbCondition_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        id_old = c.Int(),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.pcbCondition_id);
            
            CreateTable(
                "dbo.PointOfContact",
                c => new
                    {
                        pointOfContact_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.pointOfContact_id);
            
            CreateTable(
                "dbo.ProgramInformationType",
                c => new
                    {
                        programInfoType_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.programInfoType_id);
            
            CreateTable(
                "dbo.QuoteLinesOptions",
                c => new
                    {
                        quoteLineOption_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.quoteLineOption_id);
            
            CreateTable(
                "dbo.RateOurService",
                c => new
                    {
                        rateOurService_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.rateOurService_id);
            
            CreateTable(
                "dbo.SendLetterOfReference",
                c => new
                    {
                        sendLetterOfReference_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.sendLetterOfReference_id);
            
            CreateTable(
                "dbo.ServiceOrderStatus",
                c => new
                    {
                        serviceOrderStatus_id = c.Int(nullable: false, identity: true),
                        serviceOrderStatusParent_id = c.Int(),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        order = c.Int(nullable: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.serviceOrderStatus_id)
                .ForeignKey("dbo.ServiceOrderStatus", t => t.serviceOrderStatusParent_id)
                .Index(t => t.serviceOrderStatusParent_id);
            
            CreateTable(
                "dbo.ShippingMediaStatus",
                c => new
                    {
                        shippingMediaStatus_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.shippingMediaStatus_id);
            
            CreateTable(
                "dbo.ShippingMethods",
                c => new
                    {
                        shippingMethod_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 100, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.shippingMethod_id);
            
            CreateTable(
                "dbo.TechTeams",
                c => new
                    {
                        techTeam_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.techTeam_id);
            
            CreateTable(
                "dbo.TypeOfService",
                c => new
                    {
                        typeofservice_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(maxLength: 128, unicode: false),
                        id_old = c.Int(),
                        order = c.Int(),
                    })
                .PrimaryKey(t => t.typeofservice_id);
            
            CreateTable(
                "dbo.TypeOfRAID",
                c => new
                    {
                        typeOfRaid_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.typeOfRaid_id);
            
            CreateTable(
                "dbo.TypeRAIDControlled",
                c => new
                    {
                        typeRAIDControlled_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.typeRAIDControlled_id);
            
            CreateTable(
                "dbo.WouldBeAReference",
                c => new
                    {
                        wouldBeAReference_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 250, unicode: false),
                        description = c.String(maxLength: 500, unicode: false),
                        dateRegistration = c.DateTime(nullable: false),
                        userRegistration_id = c.String(maxLength: 128, unicode: false),
                        id_old = c.Int(),
                    })
                .PrimaryKey(t => t.wouldBeAReference_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceOrderStatus", "serviceOrderStatusParent_id", "dbo.ServiceOrderStatus");
            DropForeignKey("dbo.Usuario_Perfil", "id_perfil", "dbo.Perfil");
            DropForeignKey("dbo.AgentContacts", "agent_id", "dbo.Agent");
            DropForeignKey("dbo.Agent", "city_id", "dbo.City");
            DropForeignKey("dbo.AgentCommissions", "agent_id", "dbo.Agent");
            DropForeignKey("dbo.AgentCommissions", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrder", "userAssigned_id", "dbo.Usuario");
            DropForeignKey("dbo.ServiceOrder", "user_id", "dbo.Usuario");
            DropForeignKey("dbo.ServiceOrderShipping", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderRecoveryFollowUp", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderQuoting", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderMedias", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderInquiryFollowUp", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderEvaluation", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderBilling", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderContact", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.Contact", "typeContact_id", "dbo.TypeOfContact");
            DropForeignKey("dbo.SuppliersContact", "contact_id", "dbo.Contact");
            DropForeignKey("dbo.ServiceOrderContact", "contact_id", "dbo.Contact");
            DropForeignKey("dbo.CustomerContact", "contact_id", "dbo.Contact");
            DropForeignKey("dbo.ServiceOrder", "customer_id", "dbo.Customer");
            DropForeignKey("dbo.CustomerContact", "customer_id", "dbo.Customer");
            DropForeignKey("dbo.Suppliers", "city_id", "dbo.City");
            DropForeignKey("dbo.City", "state_id", "dbo.State");
            DropForeignKey("dbo.ServiceOrderShipping", "shipCity_id", "dbo.City");
            DropForeignKey("dbo.ServiceOrderBilling", "billingCity_id", "dbo.City");
            DropForeignKey("dbo.Locations", "city_id", "dbo.City");
            DropForeignKey("dbo.Usuario_Perfil", "id_usuario", "dbo.Usuario");
            DropForeignKey("dbo.Notes", "user_id", "dbo.Usuario");
            DropForeignKey("dbo.Notes", "typenote_id", "dbo.TypeOfNote");
            DropForeignKey("dbo.ServiceOrderNotes", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.ServiceOrderNotes", "note_id", "dbo.Notes");
            DropForeignKey("dbo.Usuario_Login", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "location_id", "dbo.Locations");
            DropForeignKey("dbo.LabNotes", "userRegistration_id", "dbo.Usuario");
            DropForeignKey("dbo.LabNotes", "serviceOrder_id", "dbo.ServiceOrder");
            DropForeignKey("dbo.Usuario_Claim", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.ServiceOrder", "locationReceived_id", "dbo.Locations");
            DropForeignKey("dbo.ServiceOrder", "location_id", "dbo.Locations");
            DropForeignKey("dbo.RoleLocations", "location_id", "dbo.Locations");
            DropForeignKey("dbo.RoleLocations", "id_perfil", "dbo.Perfil");
            DropForeignKey("dbo.Perfil_Area", "id_perfil", "dbo.Perfil");
            DropForeignKey("dbo.Perfil_Area", "id_area", "dbo.Area");
            DropForeignKey("dbo.Area", "id_area_mae", "dbo.Area");
            DropForeignKey("dbo.Media", "location_id", "dbo.Locations");
            DropForeignKey("dbo.Stock", "location_id", "dbo.Locations");
            DropForeignKey("dbo.Media", "supplier_id", "dbo.Suppliers");
            DropForeignKey("dbo.Stock", "media_id", "dbo.Media");
            DropForeignKey("dbo.ServiceOrderMedias", "media_id", "dbo.Media");
            DropForeignKey("dbo.PartNeeded", "media_id", "dbo.Media");
            DropForeignKey("dbo.PartNeeded", "supplier_id", "dbo.Suppliers");
            DropForeignKey("dbo.SuppliersContact", "supplier_id", "dbo.Suppliers");
            DropForeignKey("dbo.PartNeeded", "serviceOrder_id", "dbo.ServiceOrderQuoting");
            DropForeignKey("dbo.Media", "mediaStatus_id", "dbo.MediaStatus");
            DropForeignKey("dbo.Media", "manufacturer_id", "dbo.Manufacturer");
            DropForeignKey("dbo.MediaModels", "manufacturer_id", "dbo.Manufacturer");
            DropForeignKey("dbo.Stock", "component_id", "dbo.Component");
            DropForeignKey("dbo.City", "country_id", "dbo.Country");
            DropForeignKey("dbo.State", "country_id", "dbo.Country");
            DropForeignKey("dbo.Contact", "city_id", "dbo.City");
            DropForeignKey("dbo.AgentContacts", "contact_id", "dbo.Contact");
            DropIndex("dbo.ServiceOrderStatus", new[] { "serviceOrderStatusParent_id" });
            DropIndex("dbo.ServiceOrderRecoveryFollowUp", new[] { "serviceOrder_id" });
            DropIndex("dbo.ServiceOrderInquiryFollowUp", new[] { "serviceOrder_id" });
            DropIndex("dbo.ServiceOrderEvaluation", new[] { "serviceOrder_id" });
            DropIndex("dbo.CustomerContact", new[] { "contact_id" });
            DropIndex("dbo.CustomerContact", new[] { "customer_id" });
            DropIndex("dbo.ServiceOrderShipping", new[] { "shipCity_id" });
            DropIndex("dbo.ServiceOrderShipping", new[] { "serviceOrder_id" });
            DropIndex("dbo.ServiceOrderBilling", new[] { "billingCity_id" });
            DropIndex("dbo.ServiceOrderBilling", new[] { "serviceOrder_id" });
            DropIndex("dbo.ServiceOrderNotes", new[] { "serviceOrder_id" });
            DropIndex("dbo.ServiceOrderNotes", new[] { "note_id" });
            DropIndex("dbo.Notes", new[] { "user_id" });
            DropIndex("dbo.Notes", new[] { "typenote_id" });
            DropIndex("dbo.Usuario_Login", new[] { "UserId" });
            DropIndex("dbo.LabNotes", new[] { "userRegistration_id" });
            DropIndex("dbo.LabNotes", new[] { "serviceOrder_id" });
            DropIndex("dbo.Usuario_Claim", new[] { "UserId" });
            DropIndex("dbo.Usuario", "UserNameIndex");
            DropIndex("dbo.Usuario", new[] { "location_id" });
            DropIndex("dbo.Usuario_Perfil", new[] { "id_perfil" });
            DropIndex("dbo.Usuario_Perfil", new[] { "id_usuario" });
            DropIndex("dbo.Area", new[] { "id_area_mae" });
            DropIndex("dbo.Perfil_Area", new[] { "id_area" });
            DropIndex("dbo.Perfil_Area", new[] { "id_perfil" });
            DropIndex("dbo.Perfil", "RoleNameIndex");
            DropIndex("dbo.RoleLocations", new[] { "id_perfil" });
            DropIndex("dbo.RoleLocations", new[] { "location_id" });
            DropIndex("dbo.ServiceOrderMedias", new[] { "media_id" });
            DropIndex("dbo.ServiceOrderMedias", new[] { "serviceOrder_id" });
            DropIndex("dbo.SuppliersContact", new[] { "contact_id" });
            DropIndex("dbo.SuppliersContact", new[] { "supplier_id" });
            DropIndex("dbo.Suppliers", new[] { "city_id" });
            DropIndex("dbo.ServiceOrderQuoting", new[] { "serviceOrder_id" });
            DropIndex("dbo.PartNeeded", new[] { "media_id" });
            DropIndex("dbo.PartNeeded", new[] { "supplier_id" });
            DropIndex("dbo.PartNeeded", new[] { "serviceOrder_id" });
            DropIndex("dbo.MediaModels", new[] { "manufacturer_id" });
            DropIndex("dbo.Media", new[] { "mediaStatus_id" });
            DropIndex("dbo.Media", new[] { "location_id" });
            DropIndex("dbo.Media", new[] { "supplier_id" });
            DropIndex("dbo.Media", new[] { "manufacturer_id" });
            DropIndex("dbo.Stock", new[] { "location_id" });
            DropIndex("dbo.Stock", new[] { "component_id" });
            DropIndex("dbo.Stock", new[] { "media_id" });
            DropIndex("dbo.Locations", new[] { "city_id" });
            DropIndex("dbo.State", new[] { "country_id" });
            DropIndex("dbo.City", new[] { "country_id" });
            DropIndex("dbo.City", new[] { "state_id" });
            DropIndex("dbo.AgentContacts", new[] { "contact_id" });
            DropIndex("dbo.AgentContacts", new[] { "agent_id" });
            DropIndex("dbo.Contact", new[] { "city_id" });
            DropIndex("dbo.Contact", new[] { "typeContact_id" });
            DropIndex("dbo.ServiceOrderContact", new[] { "contact_id" });
            DropIndex("dbo.ServiceOrderContact", new[] { "serviceOrder_id" });
            DropIndex("dbo.ServiceOrder", new[] { "location_id" });
            DropIndex("dbo.ServiceOrder", new[] { "locationReceived_id" });
            DropIndex("dbo.ServiceOrder", new[] { "userAssigned_id" });
            DropIndex("dbo.ServiceOrder", new[] { "customer_id" });
            DropIndex("dbo.ServiceOrder", new[] { "user_id" });
            DropIndex("dbo.AgentCommissions", new[] { "agent_id" });
            DropIndex("dbo.AgentCommissions", new[] { "serviceOrder_id" });
            DropIndex("dbo.Agent", new[] { "city_id" });
            DropTable("dbo.WouldBeAReference");
            DropTable("dbo.TypeRAIDControlled");
            DropTable("dbo.TypeOfRAID");
            DropTable("dbo.TypeOfService");
            DropTable("dbo.TechTeams");
            DropTable("dbo.ShippingMethods");
            DropTable("dbo.ShippingMediaStatus");
            DropTable("dbo.ServiceOrderStatus");
            DropTable("dbo.SendLetterOfReference");
            DropTable("dbo.RateOurService");
            DropTable("dbo.QuoteLinesOptions");
            DropTable("dbo.ProgramInformationType");
            DropTable("dbo.PointOfContact");
            DropTable("dbo.PCBConditions");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.PackageConditions");
            DropTable("dbo.OpSystem");
            DropTable("dbo.MediaConditions");
            DropTable("dbo.Make");
            DropTable("dbo.JobClass");
            DropTable("dbo.Interfaces");
            DropTable("dbo.HearOfUs");
            DropTable("dbo.HDAConditions");
            DropTable("dbo.FileAlocationType");
            DropTable("dbo.FaultFound");
            DropTable("dbo.DocumentType");
            DropTable("dbo.Currency");
            DropTable("dbo.BusinessType");
            DropTable("dbo.ServiceOrderRecoveryFollowUp");
            DropTable("dbo.ServiceOrderInquiryFollowUp");
            DropTable("dbo.ServiceOrderEvaluation");
            DropTable("dbo.TypeOfContact");
            DropTable("dbo.Customer");
            DropTable("dbo.CustomerContact");
            DropTable("dbo.ServiceOrderShipping");
            DropTable("dbo.ServiceOrderBilling");
            DropTable("dbo.TypeOfNote");
            DropTable("dbo.ServiceOrderNotes");
            DropTable("dbo.Notes");
            DropTable("dbo.Usuario_Login");
            DropTable("dbo.LabNotes");
            DropTable("dbo.Usuario_Claim");
            DropTable("dbo.Usuario");
            DropTable("dbo.Usuario_Perfil");
            DropTable("dbo.Area");
            DropTable("dbo.Perfil_Area");
            DropTable("dbo.Perfil");
            DropTable("dbo.RoleLocations");
            DropTable("dbo.ServiceOrderMedias");
            DropTable("dbo.SuppliersContact");
            DropTable("dbo.Suppliers");
            DropTable("dbo.ServiceOrderQuoting");
            DropTable("dbo.PartNeeded");
            DropTable("dbo.MediaStatus");
            DropTable("dbo.MediaModels");
            DropTable("dbo.Manufacturer");
            DropTable("dbo.Media");
            DropTable("dbo.Component");
            DropTable("dbo.Stock");
            DropTable("dbo.Locations");
            DropTable("dbo.State");
            DropTable("dbo.Country");
            DropTable("dbo.City");
            DropTable("dbo.AgentContacts");
            DropTable("dbo.Contact");
            DropTable("dbo.ServiceOrderContact");
            DropTable("dbo.ServiceOrder");
            DropTable("dbo.AgentCommissions");
            DropTable("dbo.Agent");
        }
    }
}
