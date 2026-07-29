/*
IPK_RCP의 배포 스크립트

이 코드는 도구를 사용하여 생성되었습니다.
파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
변경 내용이 손실됩니다.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "IPK_RCP"
:setvar DefaultFilePrefix "IPK_RCP"
:setvar DefaultDataPath "C:\Users\kimj14\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\kimj14\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
SQLCMD 모드가 지원되지 않으면 SQLCMD 모드를 검색하고 스크립트를 실행하지 않습니다.
SQLCMD 모드를 설정한 후에 이 스크립트를 다시 사용하려면 다음을 실행합니다.
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'이 스크립트를 실행하려면 SQLCMD 모드를 사용하도록 설정해야 합니다.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'disable';


GO
PRINT N'사용자 [NT SERVICE\HealthService]을(를) 만드는 중...';


GO
--CREATE USER [NT SERVICE\HealthService] FOR LOGIN [NT Service\HealthService];


GO
PRINT N'사용자 [NT Service\SqlServerExtension]을(를) 만드는 중...';


GO
--CREATE USER [NT Service\SqlServerExtension] FOR LOGIN [NT Service\SqlServerExtension];


GO
PRINT N'역할 멤버 자격 [AW_SA]의 [db_owner]을(를) 만드는 중...';


GO
EXECUTE sp_addrolemember @rolename = N'db_owner', @membername = N'AW_SA';


GO
PRINT N'역할 멤버 자격 [NT SERVICE\HealthService]의 [SCOM_HealthService]을(를) 만드는 중...';


GO
--EXECUTE sp_addrolemember @rolename = N'SCOM_HealthService', @membername = N'NT SERVICE\HealthService';


GO
PRINT N'테이블 [dbo].[dumy]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[dumy] (
    [dumy_key]   CHAR (1) NOT NULL,
    [dumy_value] CHAR (1) NULL,
    PRIMARY KEY CLUSTERED ([dumy_key] ASC)
);


GO
PRINT N'테이블 [dbo].[hacar]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[hacar] (
    [car_no]    VARCHAR (20)    NULL,
    [dueDate]   VARCHAR (10)    NULL,
    [dueTime]   VARCHAR (8)     NULL,
    [car_desc]  VARCHAR (20)    NULL,
    [car_man]   VARCHAR (20)    NULL,
    [car_dest]  VARCHAR (100)   NULL,
    [max_vol]   DECIMAL (18, 3) NOT NULL,
    [load_vol]  DECIMAL (18, 3) NOT NULL,
    [max_qty]   DECIMAL (10, 2) NOT NULL,
    [load_qty]  DECIMAL (10, 2) NOT NULL,
    [step]      VARCHAR (1)     NOT NULL,
    [remark]    VARCHAR (100)   NULL,
    [vol_qty]   VARCHAR (1)     NULL,
    [uuse]      VARCHAR (1)     NULL,
    [area_code] VARCHAR (20)    NULL,
    [priority]  INT             NULL,
    [bachadate] VARCHAR (10)    NOT NULL,
    [seq]       INT             NOT NULL,
    [parcel]    VARCHAR (1)     NULL,
    [hdate]     VARCHAR (10)    NULL,
    [hTime]     VARCHAR (8)     NULL,
    [flag]      VARCHAR (1)     NULL,
    CONSTRAINT [PK_hacar] PRIMARY KEY CLUSTERED ([bachadate] ASC, [seq] ASC)
);


GO
PRINT N'테이블 [dbo].[haordi]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[haordi] (
    [docnum]       VARCHAR (16)    NOT NULL,
    [credat]       VARCHAR (8)     NULL,
    [cretim]       VARCHAR (6)     NULL,
    [sdno]         VARCHAR (10)    NOT NULL,
    [route]        VARCHAR (6)     NULL,
    [routedesc]    VARCHAR (MAX)   NULL,
    [deltyp]       VARCHAR (4)     NULL,
    [deltypdesc]   VARCHAR (MAX)   NULL,
    [cust]         VARCHAR (17)    NULL,
    [cust_name1]   VARCHAR (MAX)   NULL,
    [cust_name2]   VARCHAR (MAX)   NULL,
    [street]       VARCHAR (MAX)   NULL,
    [post]         VARCHAR (10)    NULL,
    [city]         VARCHAR (MAX)   NULL,
    [tel]          VARCHAR (30)    NULL,
    [contry]       VARCHAR (3)     NULL,
    [region]       VARCHAR (3)     NULL,
    [wecust]       VARCHAR (17)    NULL,
    [wecust_name1] VARCHAR (MAX)   NULL,
    [wecust_name2] VARCHAR (MAX)   NULL,
    [westreet]     VARCHAR (MAX)   NULL,
    [wepost]       VARCHAR (10)    NULL,
    [wecity]       VARCHAR (MAX)   NULL,
    [wetel]        VARCHAR (30)    NULL,
    [wecontry]     VARCHAR (3)     NULL,
    [weregion]     VARCHAR (3)     NULL,
    [duedate]      VARCHAR (8)     NULL,
    [cmmt]         VARCHAR (MAX)   NULL,
    [rmrk]         VARCHAR (MAX)   NULL,
    [parcel]       VARCHAR (1)     NULL,
    [posnr]        INT             NOT NULL,
    [matnr]        VARCHAR (18)    NULL,
    [matnrdesc]    VARCHAR (40)    NULL,
    [lgort]        VARCHAR (4)     NULL,
    [charg]        VARCHAR (10)    NULL,
    [plant]        VARCHAR (4)     NULL,
    [qty]          DECIMAL (13, 3) NULL,
    [gwgt]         DECIMAL (15, 3) NULL,
    [nwgt]         DECIMAL (15, 3) NULL,
    [wunit]        VARCHAR (3)     NULL,
    [vol]          DECIMAL (13, 3) NULL,
    [vunit]        VARCHAR (3)     NULL,
    [pstyv]        VARCHAR (4)     NULL,
    [pstyvdesc]    VARCHAR (MAX)   NULL,
    [sono]         VARCHAR (MAX)   NULL,
    [soposnr]      INT             NULL,
    [sodate]       VARCHAR (8)     NULL,
    [custpo]       VARCHAR (MAX)   NULL,
    [custpodate]   VARCHAR (8)     NULL,
    [rqty]         DECIMAL (13, 3) NULL,
    [fqty]         DECIMAL (13, 3) NULL,
    [flag]         VARCHAR (1)     NULL,
    [arrival]      VARCHAR (MAX)   NULL,
    [car_no]       VARCHAR (20)    NULL,
    [car_step]     VARCHAR (1)     NULL,
    [car_sno]      INT             NULL,
    [print_step]   VARCHAR (1)     NULL,
    [ordi_seq]     INT             NOT NULL,
    [ordi_check]   VARCHAR (20)    NULL,
    [remark]       VARCHAR (40)    NULL,
    [bachadate]    VARCHAR (10)    NULL,
    [ordi_ltqty]   DECIMAL (13, 3) NULL,
    [ordi_size]    DECIMAL (18, 3) NULL,
    [recv_dt]      DATETIME        NULL,
    [hdate]        VARCHAR (8)     NULL,
    [htime]        VARCHAR (6)     NULL,
    [vgbel]        VARCHAR (10)    NULL,
    [vsbed]        VARCHAR (2)     NULL,
    [ablad]        VARCHAR (MAX)   NULL,
    [shipno]       VARCHAR (10)    NULL,
    CONSTRAINT [PK_haordi] PRIMARY KEY CLUSTERED ([docnum] ASC, [sdno] ASC, [posnr] ASC, [ordi_seq] ASC)
);


GO
PRINT N'테이블 [dbo].[hawmto]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[hawmto] (
    [docnum]     VARCHAR (16)    NOT NULL,
    [credat]     VARCHAR (8)     NULL,
    [cretim]     VARCHAR (6)     NULL,
    [lgnum]      VARCHAR (3)     NULL,
    [tanum]      DECIMAL (10)    NOT NULL,
    [bwlvs]      VARCHAR (3)     NOT NULL,
    [trart]      VARCHAR (1)     NULL,
    [bname]      VARCHAR (12)    NULL,
    [tapos]      INT             NOT NULL,
    [matnr]      VARCHAR (18)    NULL,
    [plant]      VARCHAR (4)     NULL,
    [charg]      VARCHAR (10)    NULL,
    [bestq]      VARCHAR (1)     NULL,
    [sobkz]      VARCHAR (1)     NULL,
    [lsonr]      VARCHAR (24)    NULL,
    [meins]      VARCHAR (3)     NULL,
    [wdatu]      VARCHAR (8)     NULL,
    [wenum]      VARCHAR (10)    NULL,
    [vltyp]      VARCHAR (3)     NULL,
    [vsolm]      DECIMAL (13)    NULL,
    [nltyp]      VARCHAR (3)     NULL,
    [maktx]      VARCHAR (40)    NULL,
    [vfdat]      VARCHAR (8)     NULL,
    [lgort]      VARCHAR (4)     NULL,
    [io]         VARCHAR (1)     NULL,
    [rqty]       DECIMAL (13)    NULL,
    [fqty]       DECIMAL (13)    NULL,
    [flag]       VARCHAR (1)     NULL,
    [hdate]      VARCHAR (8)     NULL,
    [htime]      VARCHAR (6)     NULL,
    [pksz]       DECIMAL (13, 3) NULL,
    [arrival]    VARCHAR (MAX)   NULL,
    [car_no]     VARCHAR (20)    NULL,
    [car_step]   VARCHAR (1)     NULL,
    [car_sno]    INT             NULL,
    [ordi_seq]   INT             NOT NULL,
    [ordi_size]  DECIMAL (18, 3) NULL,
    [print_step] VARCHAR (1)     NULL,
    [ordi_check] VARCHAR (20)    NULL,
    [remark]     VARCHAR (MAX)   NULL,
    [bigo]       VARCHAR (MAX)   NULL,
    [bachadate]  VARCHAR (10)    NULL,
    [recv_dt]    DATETIME        NULL,
    CONSTRAINT [pk_hawmto] PRIMARY KEY CLUSTERED ([docnum] ASC, [tanum] ASC, [tapos] ASC, [ordi_seq] ASC)
);


GO
PRINT N'테이블 [dbo].[hiordi]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[hiordi] (
    [docnum]       VARCHAR (16)    NOT NULL,
    [credat]       VARCHAR (8)     NULL,
    [cretim]       VARCHAR (6)     NULL,
    [sdno]         VARCHAR (10)    NOT NULL,
    [route]        VARCHAR (6)     NULL,
    [routedesc]    VARCHAR (MAX)   NULL,
    [deltyp]       VARCHAR (4)     NULL,
    [deltypdesc]   VARCHAR (MAX)   NULL,
    [cust]         VARCHAR (17)    NULL,
    [cust_name1]   VARCHAR (MAX)   NULL,
    [cust_name2]   VARCHAR (MAX)   NULL,
    [street]       VARCHAR (MAX)   NULL,
    [post]         VARCHAR (10)    NULL,
    [city]         VARCHAR (MAX)   NULL,
    [tel]          VARCHAR (30)    NULL,
    [contry]       VARCHAR (3)     NULL,
    [region]       VARCHAR (3)     NULL,
    [wecust]       VARCHAR (17)    NULL,
    [wecust_name1] VARCHAR (MAX)   NULL,
    [wecust_name2] VARCHAR (MAX)   NULL,
    [westreet]     VARCHAR (MAX)   NULL,
    [wepost]       VARCHAR (10)    NULL,
    [wecity]       VARCHAR (MAX)   NULL,
    [wetel]        VARCHAR (30)    NULL,
    [wecontry]     VARCHAR (3)     NULL,
    [weregion]     VARCHAR (3)     NULL,
    [duedate]      VARCHAR (8)     NULL,
    [cmmt]         VARCHAR (MAX)   NULL,
    [rmrk]         VARCHAR (MAX)   NULL,
    [parcel]       VARCHAR (1)     NULL,
    [posnr]        INT             NOT NULL,
    [matnr]        VARCHAR (18)    NULL,
    [matnrdesc]    VARCHAR (40)    NULL,
    [lgort]        VARCHAR (4)     NULL,
    [charg]        VARCHAR (10)    NULL,
    [plant]        VARCHAR (4)     NULL,
    [qty]          DECIMAL (13, 3) NULL,
    [gwgt]         DECIMAL (15, 3) NULL,
    [nwgt]         DECIMAL (15, 3) NULL,
    [wunit]        VARCHAR (3)     NULL,
    [vol]          DECIMAL (13, 3) NULL,
    [vunit]        VARCHAR (3)     NULL,
    [pstyv]        VARCHAR (4)     NULL,
    [pstyvdesc]    VARCHAR (MAX)   NULL,
    [sono]         VARCHAR (MAX)   NULL,
    [soposnr]      INT             NULL,
    [sodate]       VARCHAR (8)     NULL,
    [custpo]       VARCHAR (MAX)   NULL,
    [custpodate]   VARCHAR (8)     NULL,
    [rqty]         DECIMAL (13, 3) NULL,
    [fqty]         DECIMAL (13, 3) NULL,
    [flag]         VARCHAR (1)     NULL,
    [arrival]      VARCHAR (MAX)   NULL,
    [car_no]       VARCHAR (20)    NULL,
    [car_step]     VARCHAR (1)     NULL,
    [car_sno]      INT             NULL,
    [print_step]   VARCHAR (1)     NULL,
    [ordi_seq]     INT             NOT NULL,
    [ordi_check]   VARCHAR (20)    NULL,
    [remark]       VARCHAR (40)    NULL,
    [bachadate]    VARCHAR (10)    NULL,
    [ordi_ltqty]   DECIMAL (13, 3) NULL,
    [ordi_size]    DECIMAL (18, 3) NULL,
    [recv_dt]      DATETIME        NULL,
    [hdate]        VARCHAR (8)     NULL,
    [htime]        VARCHAR (6)     NULL,
    [vgbel]        VARCHAR (10)    NULL,
    [vsbed]        VARCHAR (2)     NULL,
    [ablad]        VARCHAR (MAX)   NULL,
    [shipno]       VARCHAR (10)    NULL,
    CONSTRAINT [PK_hiordi] PRIMARY KEY CLUSTERED ([docnum] ASC, [sdno] ASC, [posnr] ASC, [ordi_seq] ASC)
);


GO
PRINT N'테이블 [dbo].[hiordx]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[hiordx] (
    [ordxkey] DECIMAL (18)    NOT NULL,
    [docnum]  VARCHAR (16)    NOT NULL,
    [sdno]    VARCHAR (10)    NOT NULL,
    [posnr]   INT             NOT NULL,
    [lstk]    VARCHAR (7)     NULL,
    [pltno]   VARCHAR (8)     NULL,
    [qty]     DECIMAL (13, 3) NULL,
    [flag]    VARCHAR (2)     NULL,
    [credat]  VARCHAR (8)     NULL,
    [cretim]  VARCHAR (6)     NULL,
    [pksz]    DECIMAL (18, 3) NULL,
    [remark]  VARCHAR (40)    NULL,
    [oprod]   VARCHAR (18)    NULL,
    [idate]   VARCHAR (10)    NULL,
    [itime]   VARCHAR (8)     NULL,
    CONSTRAINT [PK_hiordx] PRIMARY KEY CLUSTERED ([ordxkey] ASC)
);


GO
PRINT N'테이블 [dbo].[hiwmto]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[hiwmto] (
    [docnum] VARCHAR (16)    NOT NULL,
    [credat] VARCHAR (8)     NULL,
    [cretim] VARCHAR (6)     NULL,
    [lgnum]  VARCHAR (3)     NULL,
    [tanum]  DECIMAL (10)    NOT NULL,
    [bwlvs]  VARCHAR (3)     NOT NULL,
    [trart]  VARCHAR (1)     NULL,
    [bname]  VARCHAR (12)    NULL,
    [tapos]  INT             NOT NULL,
    [matnr]  VARCHAR (18)    NULL,
    [plant]  VARCHAR (4)     NULL,
    [charg]  VARCHAR (10)    NULL,
    [bestq]  VARCHAR (1)     NULL,
    [sobkz]  VARCHAR (1)     NULL,
    [lsonr]  VARCHAR (24)    NULL,
    [meins]  VARCHAR (3)     NULL,
    [wdatu]  VARCHAR (8)     NULL,
    [wenum]  VARCHAR (10)    NULL,
    [vltyp]  VARCHAR (3)     NULL,
    [vsolm]  DECIMAL (13)    NULL,
    [nltyp]  VARCHAR (3)     NULL,
    [maktx]  VARCHAR (40)    NULL,
    [vfdat]  VARCHAR (8)     NULL,
    [lgort]  VARCHAR (4)     NULL,
    [io]     VARCHAR (1)     NULL,
    [rqty]   DECIMAL (13)    NULL,
    [fqty]   DECIMAL (13)    NULL,
    [flag]   VARCHAR (1)     NULL,
    [hdate]  VARCHAR (8)     NULL,
    [htime]  VARCHAR (6)     NULL,
    [pksz]   DECIMAL (13, 3) NULL,
    CONSTRAINT [pk_hiwmto] PRIMARY KEY CLUSTERED ([docnum] ASC, [tanum] ASC, [tapos] ASC)
);


GO
PRINT N'테이블 [dbo].[hiwmtx]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[hiwmtx] (
    [wmtxkey] DECIMAL (16)    NOT NULL,
    [docnum]  VARCHAR (16)    NOT NULL,
    [tanum]   DECIMAL (10)    NOT NULL,
    [tapos]   INT             NOT NULL,
    [bwlvs]   VARCHAR (3)     NOT NULL,
    [IO]      VARCHAR (1)     NOT NULL,
    [lstk]    VARCHAR (7)     NULL,
    [pltno]   VARCHAR (8)     NULL,
    [qty]     DECIMAL (13)    NULL,
    [flag]    VARCHAR (2)     NULL,
    [credat]  VARCHAR (8)     NULL,
    [cretim]  VARCHAR (6)     NULL,
    [pksz]    DECIMAL (18, 3) NULL,
    [remark]  VARCHAR (40)    NULL,
    [oprod]   VARCHAR (18)    NULL,
    [idate]   VARCHAR (10)    NULL,
    [itime]   VARCHAR (8)     NULL,
    CONSTRAINT [pk_hiwmtx] PRIMARY KEY CLUSTERED ([wmtxkey] ASC)
);


GO
PRINT N'테이블 [dbo].[miarea]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[miarea] (
    [area_code] VARCHAR (20)  NOT NULL,
    [area_name] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_miarea] PRIMARY KEY CLUSTERED ([area_code] ASC)
);


GO
PRINT N'테이블 [dbo].[mibacha]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[mibacha] (
    [bachaDate] VARCHAR (10) NOT NULL,
    [Sno]       INT          NULL,
    CONSTRAINT [PK_mibacha] PRIMARY KEY CLUSTERED ([bachaDate] ASC)
);


GO
PRINT N'테이블 [dbo].[micust]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[micust] (
    [cust_cd]   VARCHAR (17)  NOT NULL,
    [cust_desc] VARCHAR (MAX) NULL,
    CONSTRAINT [micust_x] PRIMARY KEY NONCLUSTERED ([cust_cd] ASC)
);


GO
PRINT N'테이블 [dbo].[midest]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[midest] (
    [arrival]   VARCHAR (100) NOT NULL,
    [area_code] VARCHAR (20)  NULL,
    CONSTRAINT [PK_midest_1] PRIMARY KEY CLUSTERED ([arrival] ASC)
);


GO
PRINT N'테이블 [dbo].[mijchg]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[mijchg] (
    [seq]            DECIMAL (18)   IDENTITY (1, 1) NOT NULL,
    [plti_pltno]     VARCHAR (8)    NOT NULL,
    [plti_lstk]      VARCHAR (7)    NOT NULL,
    [plti_prod]      VARCHAR (18)   NOT NULL,
    [plti_pdesc]     VARCHAR (40)   NOT NULL,
    [plti_oprod]     VARCHAR (18)   NULL,
    [plti_loc]       VARCHAR (4)    NOT NULL,
    [plti_lot]       VARCHAR (18)   NOT NULL,
    [plti_bestq]     VARCHAR (1)    NOT NULL,
    [plti_pksz]      DECIMAL (7, 3) NULL,
    [plti_remark]    VARCHAR (40)   NULL,
    [plti_icust]     VARCHAR (40)   NULL,
    [plti_stok]      DECIMAL (8)    NULL,
    [plti_rqty]      DECIMAL (8)    NULL,
    [plti_cycl_date] VARCHAR (10)   NULL,
    [plti_idate]     VARCHAR (10)   NULL,
    [plti_itime]     VARCHAR (8)    NULL,
    [plti_flag]      VARCHAR (1)    NULL,
    [plti_label]     VARCHAR (1)    NULL,
    [plti_ctype]     VARCHAR (1)    NULL,
    [plti_12]        VARCHAR (1)    NULL,
    [plti_hdate]     VARCHAR (10)   NULL,
    [plti_htime]     VARCHAR (8)    NULL,
    PRIMARY KEY CLUSTERED ([seq] ASC)
);


GO
PRINT N'테이블 [dbo].[milstk]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[milstk] (
    [lstk_no]   VARCHAR (7) NOT NULL,
    [lstk_bk]   VARCHAR (2) NULL,
    [lstk_by]   VARCHAR (2) NULL,
    [lstk_lv]   VARCHAR (2) NULL,
    [lstk_hogi] VARCHAR (2) NULL,
    [lstk_use]  VARCHAR (1) NULL,
    [lstk_srch] VARCHAR (6) NULL,
    [lstk_flag] VARCHAR (1) NULL,
    [lstk_io]   VARCHAR (1) NULL,
    [lstk_stat] VARCHAR (2) NULL,
    [lstk_type] VARCHAR (1) NULL,
    CONSTRAINT [milstk_x] PRIMARY KEY NONCLUSTERED ([lstk_no] ASC)
);


GO
PRINT N'테이블 [dbo].[mimast]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[mimast] (
    [mast_cd]     VARCHAR (18)    NOT NULL,
    [mast_desc]   VARCHAR (40)    NULL,
    [mast_type]   VARCHAR (4)     NULL,
    [mast_grp]    VARCHAR (9)     NULL,
    [mast_old]    VARCHAR (18)    NULL,
    [mast_bunit]  VARCHAR (3)     NULL,
    [mast_szdm]   VARCHAR (32)    NULL,
    [mast_gwgt]   DECIMAL (13, 3) NULL,
    [mast_nwgt]   DECIMAL (13, 3) NULL,
    [mast_wunit]  VARCHAR (3)     NULL,
    [mast_vol]    DECIMAL (13, 3) NULL,
    [mast_vunit]  VARCHAR (3)     NULL,
    [mast_date]   VARCHAR (8)     NULL,
    [mast_time]   VARCHAR (6)     NULL,
    [mast_flag]   VARCHAR (1)     NULL,
    [mast_desc1]  VARCHAR (24)    NULL,
    [mast_canqty] INT             NULL,
    CONSTRAINT [mimast_x] PRIMARY KEY NONCLUSTERED ([mast_cd] ASC)
);


GO
PRINT N'인덱스 [dbo].[mimast].[mimast_date_idx]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimast_date_idx]
    ON [dbo].[mimast]([mast_date] ASC);


GO
PRINT N'인덱스 [dbo].[mimast].[mimast_desc_idx]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimast_desc_idx]
    ON [dbo].[mimast]([mast_desc] ASC);


GO
PRINT N'인덱스 [dbo].[mimast].[mimast_desc1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimast_desc1]
    ON [dbo].[mimast]([mast_desc1] ASC);


GO
PRINT N'인덱스 [dbo].[mimast].[mimast_idx1]을(를) 만드는 중...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [mimast_idx1]
    ON [dbo].[mimast]([mast_cd] ASC);


GO
PRINT N'인덱스 [dbo].[mimast].[mimast_idx2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimast_idx2]
    ON [dbo].[mimast]([mast_flag] ASC);


GO
PRINT N'테이블 [dbo].[mimvht]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[mimvht] (
    [mvhtkey]        DECIMAL (12)   IDENTITY (1, 1) NOT NULL,
    [mvht_io_date]   VARCHAR (10)   NOT NULL,
    [mvht_io_time]   VARCHAR (8)    NOT NULL,
    [mvht_prod]      VARCHAR (18)   NOT NULL,
    [mvht_proddesc]  VARCHAR (40)   NOT NULL,
    [mvht_loc]       VARCHAR (4)    NOT NULL,
    [mvht_lot]       VARCHAR (10)   NULL,
    [mvht_bestq]     VARCHAR (1)    NULL,
    [mvht_remark]    VARCHAR (40)   NULL,
    [mvht_pksz]      DECIMAL (7, 3) NULL,
    [mvht_ioqty]     DECIMAL (7)    NULL,
    [mvht_pltno]     VARCHAR (8)    NULL,
    [mvht_from_lstk] VARCHAR (7)    NULL,
    [mvht_to_lstk]   VARCHAR (7)    NULL,
    [mvht_ioflag]    VARCHAR (1)    NULL,
    CONSTRAINT [mimvht_x] PRIMARY KEY NONCLUSTERED ([mvhtkey] ASC)
);


GO
PRINT N'인덱스 [dbo].[mimvht].[mimvht_idx0]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimvht_idx0]
    ON [dbo].[mimvht]([mvht_io_date] ASC, [mvht_io_time] ASC);


GO
PRINT N'인덱스 [dbo].[mimvht].[mimvht_idx1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimvht_idx1]
    ON [dbo].[mimvht]([mvht_prod] ASC, [mvht_loc] ASC, [mvht_lot] ASC);


GO
PRINT N'인덱스 [dbo].[mimvht].[mimvht_idx2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimvht_idx2]
    ON [dbo].[mimvht]([mvht_loc] ASC);


GO
PRINT N'인덱스 [dbo].[mimvht].[mimvht_idx3]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimvht_idx3]
    ON [dbo].[mimvht]([mvht_lot] ASC);


GO
PRINT N'인덱스 [dbo].[mimvht].[mimvht_idx4]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [mimvht_idx4]
    ON [dbo].[mimvht]([mvht_ioflag] ASC);


GO
PRINT N'테이블 [dbo].[miordi]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[miordi] (
    [docnum]       VARCHAR (16)    NOT NULL,
    [credat]       VARCHAR (8)     NULL,
    [cretim]       VARCHAR (6)     NULL,
    [sdno]         VARCHAR (10)    NOT NULL,
    [route]        VARCHAR (6)     NULL,
    [routedesc]    VARCHAR (MAX)   NULL,
    [deltyp]       VARCHAR (4)     NULL,
    [deltypdesc]   VARCHAR (MAX)   NULL,
    [cust]         VARCHAR (17)    NULL,
    [cust_name1]   VARCHAR (MAX)   NULL,
    [cust_name2]   VARCHAR (MAX)   NULL,
    [street]       VARCHAR (MAX)   NULL,
    [post]         VARCHAR (10)    NULL,
    [city]         VARCHAR (40)    NULL,
    [tel]          VARCHAR (30)    NULL,
    [contry]       VARCHAR (3)     NULL,
    [region]       VARCHAR (3)     NULL,
    [wecust]       VARCHAR (17)    NULL,
    [wecust_name1] VARCHAR (MAX)   NULL,
    [wecust_name2] VARCHAR (MAX)   NULL,
    [westreet]     VARCHAR (MAX)   NULL,
    [wepost]       VARCHAR (10)    NULL,
    [wecity]       VARCHAR (MAX)   NULL,
    [wetel]        VARCHAR (30)    NULL,
    [wecontry]     VARCHAR (3)     NULL,
    [weregion]     VARCHAR (3)     NULL,
    [duedate]      VARCHAR (8)     NULL,
    [cmmt]         VARCHAR (MAX)   NULL,
    [rmrk]         VARCHAR (MAX)   NULL,
    [parcel]       VARCHAR (1)     NULL,
    [posnr]        INT             NOT NULL,
    [matnr]        VARCHAR (18)    NULL,
    [matnrdesc]    VARCHAR (40)    NULL,
    [lgort]        VARCHAR (4)     NULL,
    [charg]        VARCHAR (10)    NULL,
    [plant]        VARCHAR (4)     NULL,
    [qty]          DECIMAL (13, 3) NULL,
    [gwgt]         DECIMAL (15, 3) NULL,
    [nwgt]         DECIMAL (15, 3) NULL,
    [wunit]        VARCHAR (3)     NULL,
    [vol]          DECIMAL (13, 3) NULL,
    [vunit]        VARCHAR (3)     NULL,
    [pstyv]        VARCHAR (4)     NULL,
    [pstyvdesc]    VARCHAR (MAX)   NULL,
    [sono]         VARCHAR (MAX)   NULL,
    [soposnr]      INT             NULL,
    [sodate]       VARCHAR (8)     NULL,
    [custpo]       VARCHAR (MAX)   NULL,
    [custpodate]   VARCHAR (8)     NULL,
    [rqty]         DECIMAL (13, 3) NULL,
    [fqty]         DECIMAL (13, 3) NULL,
    [flag]         VARCHAR (1)     NULL,
    [arrival]      VARCHAR (MAX)   NULL,
    [car_no]       VARCHAR (20)    NULL,
    [car_step]     VARCHAR (1)     NULL,
    [car_sno]      INT             NULL,
    [print_step]   VARCHAR (1)     NULL,
    [ordi_seq]     INT             NOT NULL,
    [ordi_check]   VARCHAR (20)    NULL,
    [remark]       VARCHAR (40)    NULL,
    [bachadate]    VARCHAR (10)    NULL,
    [ordi_ltqty]   DECIMAL (13, 3) NULL,
    [ordi_size]    DECIMAL (13, 3) NULL,
    [recv_dt]      DATETIME        NULL,
    [hdate]        VARCHAR (8)     NULL,
    [htime]        VARCHAR (6)     NULL,
    [vgbel]        VARCHAR (10)    NULL,
    [vsbed]        VARCHAR (2)     NULL,
    [ablad]        VARCHAR (MAX)   NULL,
    [shipno]       VARCHAR (10)    NULL,
    CONSTRAINT [PK_miordi] PRIMARY KEY CLUSTERED ([docnum] ASC, [sdno] ASC, [posnr] ASC, [ordi_seq] ASC)
);


GO
PRINT N'인덱스 [dbo].[miordi].[miordi_index1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miordi_index1]
    ON [dbo].[miordi]([credat] ASC, [cretim] ASC);


GO
PRINT N'인덱스 [dbo].[miordi].[miordi_index2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miordi_index2]
    ON [dbo].[miordi]([matnrdesc] ASC);


GO
PRINT N'인덱스 [dbo].[miordi].[miordi_index3]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miordi_index3]
    ON [dbo].[miordi]([sdno] ASC);


GO
PRINT N'테이블 [dbo].[miplti]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[miplti] (
    [plti_pltno]     VARCHAR (8)     NOT NULL,
    [plti_lstk]      VARCHAR (7)     NOT NULL,
    [plti_prod]      VARCHAR (18)    NOT NULL,
    [plti_pdesc]     VARCHAR (40)    NOT NULL,
    [plti_oprod]     VARCHAR (18)    NULL,
    [plti_loc]       VARCHAR (4)     NOT NULL,
    [plti_lot]       VARCHAR (18)    NOT NULL,
    [plti_bestq]     VARCHAR (1)     NOT NULL,
    [plti_pksz]      DECIMAL (13, 3) NULL,
    [plti_remark]    VARCHAR (40)    NULL,
    [plti_icust]     VARCHAR (40)    NULL,
    [plti_stok]      DECIMAL (8)     NULL,
    [plti_rqty]      DECIMAL (8)     NULL,
    [plti_cycl_date] VARCHAR (10)    NULL,
    [plti_idate]     VARCHAR (10)    NULL,
    [plti_itime]     VARCHAR (8)     NULL,
    [plti_flag]      VARCHAR (1)     NULL,
    [plti_label]     VARCHAR (1)     NULL,
    CONSTRAINT [miplti_x] PRIMARY KEY CLUSTERED ([plti_pltno] ASC, [plti_lstk] ASC, [plti_prod] ASC, [plti_loc] ASC, [plti_lot] ASC, [plti_bestq] ASC)
);


GO
PRINT N'인덱스 [dbo].[miplti].[miplti_idx1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miplti_idx1]
    ON [dbo].[miplti]([plti_prod] ASC, [plti_loc] ASC, [plti_lot] ASC, [plti_bestq] ASC);


GO
PRINT N'인덱스 [dbo].[miplti].[miplti_idx3]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miplti_idx3]
    ON [dbo].[miplti]([plti_loc] ASC);


GO
PRINT N'인덱스 [dbo].[miplti].[miplti_idx4]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miplti_idx4]
    ON [dbo].[miplti]([plti_lot] ASC);


GO
PRINT N'인덱스 [dbo].[miplti].[miplti_idx5]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miplti_idx5]
    ON [dbo].[miplti]([plti_lstk] ASC);


GO
PRINT N'인덱스 [dbo].[miplti].[miplti_idx6]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miplti_idx6]
    ON [dbo].[miplti]([plti_pltno] ASC);


GO
PRINT N'인덱스 [dbo].[miplti].[miplti_idx7]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miplti_idx7]
    ON [dbo].[miplti]([plti_idate] ASC, [plti_itime] ASC);


GO
PRINT N'인덱스 [dbo].[miplti].[miplti_idx8]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miplti_idx8]
    ON [dbo].[miplti]([plti_cycl_date] ASC);


GO
PRINT N'테이블 [dbo].[miuser]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[miuser] (
    [userid]   VARCHAR (20) NOT NULL,
    [passwd]   VARCHAR (20) NOT NULL,
    [username] VARCHAR (20) NOT NULL,
    [role]     VARCHAR (1)  NULL,
    [credt]    DATETIME     NULL,
    PRIMARY KEY CLUSTERED ([userid] ASC)
);


GO
PRINT N'테이블 [dbo].[miwmto]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[miwmto] (
    [docnum] VARCHAR (16)    NOT NULL,
    [credat] VARCHAR (8)     NULL,
    [cretim] VARCHAR (6)     NULL,
    [lgnum]  VARCHAR (3)     NULL,
    [tanum]  DECIMAL (10)    NOT NULL,
    [bwlvs]  VARCHAR (3)     NOT NULL,
    [trart]  VARCHAR (1)     NULL,
    [bname]  VARCHAR (12)    NULL,
    [tapos]  INT             NOT NULL,
    [matnr]  VARCHAR (18)    NULL,
    [plant]  VARCHAR (4)     NULL,
    [charg]  VARCHAR (10)    NULL,
    [bestq]  VARCHAR (1)     NULL,
    [sobkz]  VARCHAR (1)     NULL,
    [lsonr]  VARCHAR (24)    NULL,
    [meins]  VARCHAR (3)     NULL,
    [wdatu]  VARCHAR (8)     NULL,
    [wenum]  VARCHAR (10)    NULL,
    [vltyp]  VARCHAR (3)     NULL,
    [vsolm]  DECIMAL (13)    NULL,
    [nltyp]  VARCHAR (3)     NULL,
    [maktx]  VARCHAR (40)    NULL,
    [vfdat]  VARCHAR (8)     NULL,
    [lgort]  VARCHAR (4)     NULL,
    [io]     VARCHAR (1)     NULL,
    [rqty]   DECIMAL (13)    NULL,
    [fqty]   DECIMAL (13)    NULL,
    [flag]   VARCHAR (1)     NULL,
    [hdate]  VARCHAR (8)     NULL,
    [htime]  VARCHAR (6)     NULL,
    [pksz]   DECIMAL (13, 3) NULL,
    CONSTRAINT [pk_miwmto] PRIMARY KEY CLUSTERED ([docnum] ASC, [tanum] ASC, [tapos] ASC)
);


GO
PRINT N'인덱스 [dbo].[miwmto].[miwmto_idx1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miwmto_idx1]
    ON [dbo].[miwmto]([credat] ASC, [cretim] ASC);


GO
PRINT N'인덱스 [dbo].[miwmto].[miwmto_idx2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miwmto_idx2]
    ON [dbo].[miwmto]([bwlvs] ASC);


GO
PRINT N'인덱스 [dbo].[miwmto].[miwmto_idx3]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [miwmto_idx3]
    ON [dbo].[miwmto]([io] ASC);


GO
PRINT N'테이블 [dbo].[oflnoupt]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[oflnoupt] (
    [ofln_date] CHAR (10) NOT NULL,
    [ofln_time] CHAR (8)  NOT NULL,
    [ofln_loca] CHAR (7)  NULL,
    [ofln_flag] CHAR (1)  NULL,
    CONSTRAINT [oflnoupt_key] PRIMARY KEY CLUSTERED ([ofln_date] ASC, [ofln_time] ASC)
);


GO
PRINT N'테이블 [dbo].[pbcatcol]을(를) 만드는 중...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
CREATE TABLE [dbo].[pbcatcol] (
    [pbc_tnam] CHAR (30)     NULL,
    [pbc_tid]  INT           NULL,
    [pbc_ownr] CHAR (30)     NULL,
    [pbc_cnam] CHAR (30)     NULL,
    [pbc_cid]  SMALLINT      NULL,
    [pbc_labl] VARCHAR (254) NULL,
    [pbc_lpos] SMALLINT      NULL,
    [pbc_hdr]  VARCHAR (254) NULL,
    [pbc_hpos] SMALLINT      NULL,
    [pbc_jtfy] SMALLINT      NULL,
    [pbc_mask] VARCHAR (31)  NULL,
    [pbc_case] SMALLINT      NULL,
    [pbc_hght] SMALLINT      NULL,
    [pbc_wdth] SMALLINT      NULL,
    [pbc_ptrn] VARCHAR (31)  NULL,
    [pbc_bmap] CHAR (1)      NULL,
    [pbc_init] VARCHAR (254) NULL,
    [pbc_cmnt] VARCHAR (254) NULL,
    [pbc_edit] VARCHAR (31)  NULL,
    [pbc_tag]  VARCHAR (254) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'인덱스 [dbo].[pbcatcol].[pbcatcol_idx]을(를) 만드는 중...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [pbcatcol_idx]
    ON [dbo].[pbcatcol]([pbc_tnam] ASC, [pbc_ownr] ASC, [pbc_cnam] ASC);


GO
PRINT N'테이블 [dbo].[pbcatedt]을(를) 만드는 중...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
CREATE TABLE [dbo].[pbcatedt] (
    [pbe_name] VARCHAR (30)  NOT NULL,
    [pbe_edit] VARCHAR (254) NULL,
    [pbe_type] SMALLINT      NOT NULL,
    [pbe_cntr] INT           NULL,
    [pbe_seqn] SMALLINT      NOT NULL,
    [pbe_flag] INT           NULL,
    [pbe_work] CHAR (32)     NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'인덱스 [dbo].[pbcatedt].[pbcatedt_idx]을(를) 만드는 중...';


GO
CREATE UNIQUE CLUSTERED INDEX [pbcatedt_idx]
    ON [dbo].[pbcatedt]([pbe_name] ASC, [pbe_seqn] ASC);


GO
PRINT N'테이블 [dbo].[pbcatfmt]을(를) 만드는 중...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
CREATE TABLE [dbo].[pbcatfmt] (
    [pbf_name] VARCHAR (30)  NOT NULL,
    [pbf_frmt] VARCHAR (254) NOT NULL,
    [pbf_type] SMALLINT      NOT NULL,
    [pbf_cntr] INT           NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'인덱스 [dbo].[pbcatfmt].[pbcatfmt_idx]을(를) 만드는 중...';


GO
CREATE UNIQUE CLUSTERED INDEX [pbcatfmt_idx]
    ON [dbo].[pbcatfmt]([pbf_name] ASC);


GO
PRINT N'테이블 [dbo].[pbcattbl]을(를) 만드는 중...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
CREATE TABLE [dbo].[pbcattbl] (
    [pbt_tnam] CHAR (30)     NULL,
    [pbt_tid]  INT           NULL,
    [pbt_ownr] CHAR (30)     NULL,
    [pbd_fhgt] SMALLINT      NULL,
    [pbd_fwgt] SMALLINT      NULL,
    [pbd_fitl] CHAR (1)      NULL,
    [pbd_funl] CHAR (1)      NULL,
    [pbd_fchr] SMALLINT      NULL,
    [pbd_fptc] SMALLINT      NULL,
    [pbd_ffce] CHAR (32)     NULL,
    [pbh_fhgt] SMALLINT      NULL,
    [pbh_fwgt] SMALLINT      NULL,
    [pbh_fitl] CHAR (1)      NULL,
    [pbh_funl] CHAR (1)      NULL,
    [pbh_fchr] SMALLINT      NULL,
    [pbh_fptc] SMALLINT      NULL,
    [pbh_ffce] CHAR (32)     NULL,
    [pbl_fhgt] SMALLINT      NULL,
    [pbl_fwgt] SMALLINT      NULL,
    [pbl_fitl] CHAR (1)      NULL,
    [pbl_funl] CHAR (1)      NULL,
    [pbl_fchr] SMALLINT      NULL,
    [pbl_fptc] SMALLINT      NULL,
    [pbl_ffce] CHAR (32)     NULL,
    [pbt_cmnt] VARCHAR (254) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'인덱스 [dbo].[pbcattbl].[pbcattbl_idx]을(를) 만드는 중...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [pbcattbl_idx]
    ON [dbo].[pbcattbl]([pbt_tnam] ASC, [pbt_ownr] ASC);


GO
PRINT N'테이블 [dbo].[pbcatvld]을(를) 만드는 중...';


GO
SET QUOTED_IDENTIFIER ON;

SET ANSI_NULLS OFF;


GO
CREATE TABLE [dbo].[pbcatvld] (
    [pbv_name] VARCHAR (30)  NOT NULL,
    [pbv_vald] VARCHAR (254) NOT NULL,
    [pbv_type] SMALLINT      NOT NULL,
    [pbv_cntr] INT           NULL,
    [pbv_msg]  VARCHAR (254) NULL
);


GO
SET ANSI_NULLS, QUOTED_IDENTIFIER ON;


GO
PRINT N'인덱스 [dbo].[pbcatvld].[pbcatvld_idx]을(를) 만드는 중...';


GO
CREATE UNIQUE CLUSTERED INDEX [pbcatvld_idx]
    ON [dbo].[pbcatvld]([pbv_name] ASC);


GO
PRINT N'테이블 [dbo].[tacar]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tacar] (
    [car_no]    VARCHAR (20)    NOT NULL,
    [dueDate]   VARCHAR (10)    NULL,
    [dueTime]   VARCHAR (8)     NULL,
    [car_desc]  VARCHAR (20)    NULL,
    [car_man]   VARCHAR (20)    NULL,
    [car_dest]  VARCHAR (100)   NULL,
    [max_vol]   DECIMAL (18, 3) NULL,
    [load_vol]  DECIMAL (18, 3) NULL,
    [max_qty]   DECIMAL (10, 2) NULL,
    [load_qty]  DECIMAL (10, 2) NULL,
    [step]      VARCHAR (1)     NULL,
    [remark]    VARCHAR (100)   NULL,
    [vol_qty]   VARCHAR (1)     NULL,
    [uuse]      VARCHAR (1)     NULL,
    [area_code] VARCHAR (20)    NULL,
    [priority]  INT             NULL,
    [bachadate] VARCHAR (10)    NULL,
    [seq]       INT             NULL,
    [parcel]    VARCHAR (1)     NULL,
    [hdate]     VARCHAR (10)    NULL,
    [hTime]     VARCHAR (8)     NULL,
    [flag]      VARCHAR (1)     NULL,
    CONSTRAINT [PK_tacar] PRIMARY KEY CLUSTERED ([car_no] ASC)
);


GO
PRINT N'테이블 [dbo].[taordi]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[taordi] (
    [docnum]       VARCHAR (16)    NOT NULL,
    [credat]       VARCHAR (8)     NULL,
    [cretim]       VARCHAR (6)     NULL,
    [sdno]         VARCHAR (10)    NOT NULL,
    [route]        VARCHAR (6)     NULL,
    [routedesc]    VARCHAR (MAX)   NULL,
    [deltyp]       VARCHAR (4)     NULL,
    [deltypdesc]   VARCHAR (MAX)   NULL,
    [cust]         VARCHAR (17)    NULL,
    [cust_name1]   VARCHAR (MAX)   NULL,
    [cust_name2]   VARCHAR (MAX)   NULL,
    [street]       VARCHAR (MAX)   NULL,
    [post]         VARCHAR (10)    NULL,
    [city]         VARCHAR (MAX)   NULL,
    [tel]          VARCHAR (30)    NULL,
    [contry]       VARCHAR (3)     NULL,
    [region]       VARCHAR (3)     NULL,
    [wecust]       VARCHAR (17)    NULL,
    [wecust_name1] VARCHAR (MAX)   NULL,
    [wecust_name2] VARCHAR (MAX)   NULL,
    [westreet]     VARCHAR (MAX)   NULL,
    [wepost]       VARCHAR (10)    NULL,
    [wecity]       VARCHAR (MAX)   NULL,
    [wetel]        VARCHAR (30)    NULL,
    [wecontry]     VARCHAR (3)     NULL,
    [weregion]     VARCHAR (3)     NULL,
    [duedate]      VARCHAR (8)     NULL,
    [cmmt]         VARCHAR (MAX)   NULL,
    [rmrk]         VARCHAR (MAX)   NULL,
    [parcel]       VARCHAR (1)     NULL,
    [posnr]        INT             NOT NULL,
    [matnr]        VARCHAR (18)    NULL,
    [matnrdesc]    VARCHAR (40)    NULL,
    [lgort]        VARCHAR (4)     NULL,
    [charg]        VARCHAR (10)    NULL,
    [plant]        VARCHAR (4)     NULL,
    [qty]          DECIMAL (13, 3) NULL,
    [gwgt]         DECIMAL (15, 3) NULL,
    [nwgt]         DECIMAL (15, 3) NULL,
    [wunit]        VARCHAR (3)     NULL,
    [vol]          DECIMAL (13, 3) NULL,
    [vunit]        VARCHAR (3)     NULL,
    [pstyv]        VARCHAR (4)     NULL,
    [pstyvdesc]    VARCHAR (MAX)   NULL,
    [sono]         VARCHAR (MAX)   NULL,
    [soposnr]      INT             NULL,
    [sodate]       VARCHAR (8)     NULL,
    [custpo]       VARCHAR (MAX)   NULL,
    [custpodate]   VARCHAR (8)     NULL,
    [rqty]         DECIMAL (13, 3) NULL,
    [fqty]         DECIMAL (13, 3) NULL,
    [flag]         VARCHAR (1)     NULL,
    [arrival]      VARCHAR (MAX)   NULL,
    [car_no]       VARCHAR (20)    NULL,
    [car_step]     VARCHAR (1)     NULL,
    [car_sno]      INT             NULL,
    [print_step]   VARCHAR (1)     NULL,
    [ordi_seq]     INT             NOT NULL,
    [ordi_check]   VARCHAR (20)    NULL,
    [remark]       VARCHAR (40)    NULL,
    [bachadate]    VARCHAR (10)    NULL,
    [ordi_ltqty]   DECIMAL (13, 3) NULL,
    [ordi_size]    DECIMAL (18, 3) NULL,
    [recv_dt]      DATETIME        NULL,
    [hdate]        VARCHAR (8)     NULL,
    [htime]        VARCHAR (6)     NULL,
    [vgbel]        VARCHAR (10)    NULL,
    [vsbed]        VARCHAR (2)     NULL,
    [ablad]        VARCHAR (MAX)   NULL,
    [shipno]       VARCHAR (10)    NULL,
    CONSTRAINT [PK_taordi] PRIMARY KEY CLUSTERED ([docnum] ASC, [sdno] ASC, [posnr] ASC, [ordi_seq] ASC)
);


GO
PRINT N'인덱스 [dbo].[taordi].[taordi_index1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [taordi_index1]
    ON [dbo].[taordi]([credat] ASC, [cretim] ASC);


GO
PRINT N'인덱스 [dbo].[taordi].[taordi_index2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [taordi_index2]
    ON [dbo].[taordi]([matnrdesc] ASC);


GO
PRINT N'인덱스 [dbo].[taordi].[taordi_index3]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [taordi_index3]
    ON [dbo].[taordi]([sdno] ASC);


GO
PRINT N'테이블 [dbo].[tawmto]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tawmto] (
    [docnum]     VARCHAR (16)    NOT NULL,
    [credat]     VARCHAR (8)     NULL,
    [cretim]     VARCHAR (6)     NULL,
    [lgnum]      VARCHAR (3)     NULL,
    [tanum]      DECIMAL (10)    NOT NULL,
    [bwlvs]      VARCHAR (3)     NOT NULL,
    [trart]      VARCHAR (1)     NULL,
    [bname]      VARCHAR (12)    NULL,
    [tapos]      INT             NOT NULL,
    [matnr]      VARCHAR (18)    NULL,
    [plant]      VARCHAR (4)     NULL,
    [charg]      VARCHAR (10)    NULL,
    [bestq]      VARCHAR (1)     NULL,
    [sobkz]      VARCHAR (1)     NULL,
    [lsonr]      VARCHAR (24)    NULL,
    [meins]      VARCHAR (3)     NULL,
    [wdatu]      VARCHAR (8)     NULL,
    [wenum]      VARCHAR (10)    NULL,
    [vltyp]      VARCHAR (3)     NULL,
    [vsolm]      DECIMAL (13)    NULL,
    [nltyp]      VARCHAR (3)     NULL,
    [maktx]      VARCHAR (40)    NULL,
    [vfdat]      VARCHAR (8)     NULL,
    [lgort]      VARCHAR (4)     NULL,
    [io]         VARCHAR (1)     NULL,
    [rqty]       DECIMAL (13)    NULL,
    [fqty]       DECIMAL (13)    NULL,
    [flag]       VARCHAR (1)     NULL,
    [hdate]      VARCHAR (8)     NULL,
    [htime]      VARCHAR (6)     NULL,
    [pksz]       DECIMAL (13, 3) NULL,
    [arrival]    VARCHAR (MAX)   NULL,
    [car_no]     VARCHAR (20)    NULL,
    [car_step]   VARCHAR (1)     NULL,
    [car_sno]    INT             NULL,
    [ordi_seq]   INT             NOT NULL,
    [ordi_size]  DECIMAL (18, 3) NULL,
    [print_step] VARCHAR (1)     NULL,
    [ordi_check] VARCHAR (20)    NULL,
    [remark]     VARCHAR (MAX)   NULL,
    [bigo]       VARCHAR (MAX)   NULL,
    [bachadate]  VARCHAR (10)    NULL,
    [recv_dt]    DATETIME        NULL,
    CONSTRAINT [pk_tawmto] PRIMARY KEY CLUSTERED ([docnum] ASC, [tanum] ASC, [tapos] ASC, [ordi_seq] ASC)
);


GO
PRINT N'테이블 [dbo].[tbberr]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbberr] (
    [err_date]  VARCHAR (10)  NOT NULL,
    [err_time]  VARCHAR (8)   NOT NULL,
    [err_pltno] VARCHAR (8)   NULL,
    [err_msg]   VARCHAR (100) NULL,
    [err_act]   VARCHAR (1)   NULL,
    [err_mmsg]  VARCHAR (40)  NULL,
    CONSTRAINT [PK_tbberr] PRIMARY KEY CLUSTERED ([err_date] ASC, [err_time] ASC)
);


GO
PRINT N'테이블 [dbo].[tbbprn]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbbprn] (
    [prn_no]     VARCHAR (1)     NOT NULL,
    [prn_pltno]  VARCHAR (8)     NOT NULL,
    [prn_prod]   VARCHAR (18)    NULL,
    [prn_pdesc]  VARCHAR (40)    NULL,
    [prn_lot]    VARCHAR (18)    NULL,
    [prn_pksz]   DECIMAL (18, 3) NULL,
    [prn_qty]    INT             NULL,
    [prn_mixcnt] INT             NULL,
    [prn_date]   DATETIME        NULL,
    [prn_flag]   VARCHAR (1)     NULL,
    CONSTRAINT [tbbprn_key] PRIMARY KEY NONCLUSTERED ([prn_no] ASC, [prn_pltno] ASC)
);


GO
PRINT N'테이블 [dbo].[tbcnvc]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbcnvc] (
    [cnvc_mode]     VARCHAR (2)   NOT NULL,
    [cnvc_ch01]     VARCHAR (16)  NULL,
    [cnvc_ch02]     VARCHAR (16)  NULL,
    [cnvc_ch03]     VARCHAR (16)  NULL,
    [cnvc_ch04]     VARCHAR (16)  NULL,
    [cnvc_ch05]     VARCHAR (16)  NULL,
    [cnvc_ch06]     VARCHAR (16)  NULL,
    [cnvc_op_onof]  VARCHAR (8)   NULL,
    [cnvc_op_eror]  VARCHAR (8)   NULL,
    [cnvc_job_no]   VARCHAR (60)  NULL,
    [cnvc_jobno]    VARCHAR (188) NULL,
    [cnvc_buf_palt] VARCHAR (50)  NULL,
    [cnvc_ist_redy] VARCHAR (5)   NULL,
    [cnvc_ist_palt] VARCHAR (5)   NULL,
    [cnvc_ost_redy] VARCHAR (5)   NULL,
    [cnvc_ost_palt] VARCHAR (5)   NULL,
    [cnvc_21_rqst]  VARCHAR (1)   NULL,
    [cnvc_22_rqst]  VARCHAR (1)   NULL,
    [cnvc_remote]   VARCHAR (1)   NULL,
    [cnvc_stop]     VARCHAR (1)   NULL,
    [cnvc_comm]     VARCHAR (1)   NULL,
    [cnvc_24_rqst]  VARCHAR (1)   NULL,
    CONSTRAINT [pk_cnvc] PRIMARY KEY CLUSTERED ([cnvc_mode] ASC)
);


GO
PRINT N'테이블 [dbo].[tberht]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tberht] (
    [erht_date] VARCHAR (8)  NOT NULL,
    [erht_time] VARCHAR (6)  NOT NULL,
    [erht_hogi] VARCHAR (2)  NOT NULL,
    [erht_ercd] VARCHAR (4)  NULL,
    [erht_mesg] VARCHAR (40) NULL,
    [erht_jno]  VARCHAR (18) NULL,
    [erht_indx] VARCHAR (4)  NULL,
    [erht_gubn] VARCHAR (1)  NULL,
    [erht_jio]  VARCHAR (1)  NULL,
    [erht_pltn] VARCHAR (8)  NULL,
    [erht_lstk] VARCHAR (7)  NULL,
    [erht_pos]  VARCHAR (4)  NULL,
    [erht_xmov] VARCHAR (1)  NULL,
    CONSTRAINT [pk_erht] PRIMARY KEY CLUSTERED ([erht_date] ASC, [erht_time] ASC, [erht_hogi] ASC)
);


GO
PRINT N'인덱스 [dbo].[tberht].[tberht_idx1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tberht_idx1]
    ON [dbo].[tberht]([erht_hogi] ASC, [erht_date] ASC, [erht_time] ASC);


GO
PRINT N'인덱스 [dbo].[tberht].[tberht_idx2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tberht_idx2]
    ON [dbo].[tberht]([erht_hogi] ASC, [erht_xmov] ASC, [erht_date] ASC, [erht_time] ASC);


GO
PRINT N'테이블 [dbo].[tbevnt]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbevnt] (
    [evnt_key]   DECIMAL (18) IDENTITY (1, 1) NOT NULL,
    [evnt_gubn]  VARCHAR (1)  NULL,
    [evnt_jio]   VARCHAR (1)  NULL,
    [evnt_hogi]  VARCHAR (1)  NULL,
    [evnt_fstn]  VARCHAR (2)  NULL,
    [evnt_tstn]  VARCHAR (2)  NULL,
    [evnt_pltn]  VARCHAR (8)  NULL,
    [evnt_lstk]  VARCHAR (7)  NULL,
    [evnt_xmov]  VARCHAR (1)  NULL,
    [evnt_sflg]  VARCHAR (1)  NULL,
    [evnt_wflg]  VARCHAR (2)  NULL,
    [evnt_uflg]  VARCHAR (1)  NULL,
    [evnt_wdate] VARCHAR (14) NULL,
    CONSTRAINT [pk_evnt] PRIMARY KEY CLUSTERED ([evnt_key] ASC)
);


GO
PRINT N'인덱스 [dbo].[tbevnt].[tbevnt_idx1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbevnt_idx1]
    ON [dbo].[tbevnt]([evnt_gubn] ASC, [evnt_jio] ASC, [evnt_xmov] ASC, [evnt_wflg] ASC, [evnt_uflg] ASC);


GO
PRINT N'인덱스 [dbo].[tbevnt].[tbevnt_idx2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbevnt_idx2]
    ON [dbo].[tbevnt]([evnt_pltn] ASC, [evnt_lstk] ASC);


GO
PRINT N'인덱스 [dbo].[tbevnt].[tbevnt_idx3]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbevnt_idx3]
    ON [dbo].[tbevnt]([evnt_hogi] ASC);


GO
PRINT N'테이블 [dbo].[tbhogi]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbhogi] (
    [hogi_key] CHAR (1) NOT NULL,
    [hogi_no]  INT      NULL,
    [hogi_no2] INT      NULL,
    PRIMARY KEY CLUSTERED ([hogi_key] ASC)
);


GO
PRINT N'테이블 [dbo].[tbindx]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbindx] (
    [indx_jno]  VARCHAR (18) NOT NULL,
    [indx_indx] VARCHAR (4)  NULL,
    [indx_gubn] VARCHAR (1)  NULL,
    [indx_jio]  VARCHAR (1)  NULL,
    [indx_hogi] VARCHAR (1)  NULL,
    [indx_fstn] VARCHAR (2)  NULL,
    [indx_tstn] VARCHAR (2)  NULL,
    [indx_pltn] VARCHAR (8)  NULL,
    [indx_lstk] VARCHAR (7)  NULL,
    [indx_xmov] VARCHAR (1)  NULL,
    [indx_edat] VARCHAR (14) NULL,
    [indx_sflg] VARCHAR (1)  NULL,
    [indx_uflg] VARCHAR (1)  NULL,
    CONSTRAINT [pk_indx] PRIMARY KEY CLUSTERED ([indx_jno] ASC)
);


GO
PRINT N'인덱스 [dbo].[tbindx].[tbindx_idx1]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbindx_idx1]
    ON [dbo].[tbindx]([indx_jio] ASC, [indx_indx] ASC, [indx_fstn] ASC, [indx_sflg] ASC);


GO
PRINT N'인덱스 [dbo].[tbindx].[tbindx_idx2]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbindx_idx2]
    ON [dbo].[tbindx]([indx_jio] ASC, [indx_indx] ASC, [indx_tstn] ASC, [indx_sflg] ASC);


GO
PRINT N'인덱스 [dbo].[tbindx].[tbindx_idx3]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbindx_idx3]
    ON [dbo].[tbindx]([indx_fstn] ASC, [indx_sflg] ASC, [indx_jno] ASC);


GO
PRINT N'인덱스 [dbo].[tbindx].[tbindx_idx4]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbindx_idx4]
    ON [dbo].[tbindx]([indx_pltn] ASC, [indx_lstk] ASC);


GO
PRINT N'인덱스 [dbo].[tbindx].[tbindx_idx5]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbindx_idx5]
    ON [dbo].[tbindx]([indx_gubn] ASC, [indx_jio] ASC, [indx_jno] ASC);


GO
PRINT N'인덱스 [dbo].[tbindx].[tbindx_idx6]을(를) 만드는 중...';


GO
CREATE NONCLUSTERED INDEX [tbindx_idx6]
    ON [dbo].[tbindx]([indx_hogi] ASC);


GO
PRINT N'테이블 [dbo].[tblock]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tblock] (
    [lock_key]  CHAR (3) NOT NULL,
    [lock_proc] INT      NULL,
    CONSTRAINT [pk_lock] PRIMARY KEY CLUSTERED ([lock_key] ASC)
);


GO
PRINT N'테이블 [dbo].[tbscer]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbscer] (
    [scer_ercd] VARCHAR (4)  NOT NULL,
    [scer_mesg] VARCHAR (60) NULL,
    CONSTRAINT [pk_scer] PRIMARY KEY CLUSTERED ([scer_ercd] ASC)
);


GO
PRINT N'테이블 [dbo].[tbscrc]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbscrc] (
    [scrc_no]    VARCHAR (2)  NOT NULL,
    [scrc_mode]  VARCHAR (2)  NULL,
    [scrc_gubn]  VARCHAR (1)  NULL,
    [scrc_io]    VARCHAR (1)  NULL,
    [scrc_onln]  VARCHAR (1)  NULL,
    [scrc_pwron] VARCHAR (1)  NULL,
    [scrc_emer]  VARCHAR (1)  NULL,
    [scrc_stat]  VARCHAR (4)  NULL,
    [scrc_palt]  VARCHAR (1)  NULL,
    [scrc_posi]  VARCHAR (4)  NULL,
    [scrc_eror]  VARCHAR (1)  NULL,
    [scrc_ecod]  VARCHAR (4)  NULL,
    [scrc_stop]  VARCHAR (1)  NULL,
    [scrc_iuse]  VARCHAR (1)  NULL,
    [scrc_ouse]  VARCHAR (1)  NULL,
    [scrc_lstk]  VARCHAR (6)  NULL,
    [scrc_pltn]  VARCHAR (8)  NULL,
    [scrc_jno]   VARCHAR (18) NULL,
    [scrc_indx]  VARCHAR (4)  NULL,
    [scrc_fstn]  VARCHAR (2)  NULL,
    [scrc_tstn]  VARCHAR (2)  NULL,
    [scrc_xmov]  VARCHAR (1)  NULL,
    [scrc_mesg]  VARCHAR (60) NULL,
    [scrc_chdt]  VARCHAR (44) NULL,
    [scrc_comm]  VARCHAR (1)  NULL,
    [scrc_rset]  VARCHAR (1)  NULL,
    CONSTRAINT [pk_scrc] PRIMARY KEY CLUSTERED ([scrc_no] ASC)
);


GO
PRINT N'테이블 [dbo].[tbseqn]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbseqn] (
    [seqn_key]  CHAR (1) NOT NULL,
    [seqn_date] CHAR (8) NOT NULL,
    [seqn_no]   INT      NULL,
    CONSTRAINT [pk_seqn] PRIMARY KEY CLUSTERED ([seqn_key] ASC)
);


GO
PRINT N'테이블 [dbo].[tbstat]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tbstat] (
    [stat_key]       VARCHAR (1) NOT NULL,
    [stat_imode]     VARCHAR (1) NULL,
    [stat_ipath]     VARCHAR (1) NULL,
    [stat_barm]      VARCHAR (1) NULL,
    [stat_dplt]      VARCHAR (1) NULL,
    [stat_lr]        VARCHAR (1) NULL,
    [stat_out]       VARCHAR (1) NULL,
    [stat_auto_load] VARCHAR (1) NULL,
    [stat_resp_load] VARCHAR (1) NULL,
    CONSTRAINT [pk_tbstat] PRIMARY KEY CLUSTERED ([stat_key] ASC)
);


GO
PRINT N'테이블 [dbo].[testlen]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[testlen] (
    [Id] INT           NOT NULL,
    [a]  TEXT          NULL,
    [b]  VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'테이블 [dbo].[tibarc]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tibarc] (
    [barc_key]   VARCHAR (1)  NOT NULL,
    [barc_pltno] VARCHAR (8)  NULL,
    [barc_date]  DATETIME     NULL,
    [barc_msg]   VARCHAR (50) NULL,
    [barc_flag]  VARCHAR (1)  NULL,
    [cvc_msg]    VARCHAR (50) NULL,
    [cvc_flag]   VARCHAR (1)  NULL,
    PRIMARY KEY CLUSTERED ([barc_key] ASC)
);


GO
PRINT N'테이블 [dbo].[tilock]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tilock] (
    [lock_key] NCHAR (1) NOT NULL,
    [lock_cnt] INT       NULL,
    CONSTRAINT [PK_tilock] PRIMARY KEY CLUSTERED ([lock_key] ASC)
);


GO
PRINT N'테이블 [dbo].[tiordx]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tiordx] (
    [ordxkey] DECIMAL (18)    IDENTITY (1, 1) NOT NULL,
    [docnum]  VARCHAR (16)    NOT NULL,
    [sdno]    VARCHAR (10)    NOT NULL,
    [posnr]   INT             NOT NULL,
    [lstk]    VARCHAR (7)     NULL,
    [pltno]   VARCHAR (8)     NULL,
    [qty]     DECIMAL (13)    NULL,
    [flag]    VARCHAR (2)     NULL,
    [credat]  VARCHAR (8)     NULL,
    [cretim]  VARCHAR (6)     NULL,
    [pksz]    DECIMAL (18, 3) NULL,
    [remark]  VARCHAR (40)    NULL,
    [oprod]   VARCHAR (18)    NULL,
    [idate]   VARCHAR (10)    NULL,
    [itime]   VARCHAR (8)     NULL,
    CONSTRAINT [PK_tiordx] PRIMARY KEY CLUSTERED ([ordxkey] ASC)
);


GO
PRINT N'테이블 [dbo].[tipltn]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tipltn] (
    [pltn_key] VARCHAR (1) NOT NULL,
    [pltno]    VARCHAR (8) NULL,
    CONSTRAINT [tipltn_x] PRIMARY KEY NONCLUSTERED ([pltn_key] ASC)
);


GO
PRINT N'테이블 [dbo].[tiwmtx]을(를) 만드는 중...';


GO
CREATE TABLE [dbo].[tiwmtx] (
    [wmtxkey] DECIMAL (16)    IDENTITY (1, 1) NOT NULL,
    [docnum]  VARCHAR (16)    NOT NULL,
    [tanum]   DECIMAL (10)    NOT NULL,
    [tapos]   INT             NOT NULL,
    [bwlvs]   VARCHAR (3)     NOT NULL,
    [IO]      VARCHAR (1)     NOT NULL,
    [lstk]    VARCHAR (7)     NULL,
    [pltno]   VARCHAR (8)     NULL,
    [qty]     DECIMAL (13)    NULL,
    [flag]    VARCHAR (2)     NULL,
    [credat]  VARCHAR (8)     NULL,
    [cretim]  VARCHAR (6)     NULL,
    [pksz]    DECIMAL (18, 3) NULL,
    [remark]  VARCHAR (40)    NULL,
    [oprod]   VARCHAR (18)    NULL,
    [idate]   VARCHAR (10)    NULL,
    [itime]   VARCHAR (8)     NULL,
    CONSTRAINT [pk_tiwmtx] PRIMARY KEY CLUSTERED ([wmtxkey] ASC)
);


GO
PRINT N'DEFAULT 제약 조건 [dbo].[dumy]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[dumy]
    ADD DEFAULT ('1') FOR [dumy_value];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [dueTime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [dueDate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [car_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [car_desc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ((0)) FOR [max_vol];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ((0)) FOR [load_vol];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [hTime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [hdate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('0') FOR [parcel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ((200)) FOR [priority];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [area_code];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [car_man];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [car_dest];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ((0)) FOR [max_qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ((0)) FOR [load_qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('0') FOR [step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('0') FOR [uuse];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hacar]
    ADD DEFAULT ('V') FOR [vol_qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [parcel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ((0)) FOR [rqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ((0)) FOR [fqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [ablad];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [arrival];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [cmmt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [rmrk];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [car_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [shipno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('0') FOR [car_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ((0)) FOR [car_sno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('0') FOR [print_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ((0)) FOR [ordi_seq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [bachadate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [ordi_check];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ((0)) FOR [ordi_size];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT (getdate()) FOR [recv_dt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [hdate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [htime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [vgbel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ('') FOR [vsbed];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[haordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[haordi]
    ADD DEFAULT ((0)) FOR [ordi_ltqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [print_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [ordi_check];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ((0)) FOR [fqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ((0)) FOR [rqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('0') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ((0)) FOR [ordi_seq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ((0)) FOR [ordi_size];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [car_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ((0)) FOR [car_sno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [arrival];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [car_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [bigo];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT ('') FOR [bachadate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hawmto]
    ADD DEFAULT (getdate()) FOR [recv_dt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [ablad];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [credat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [arrival];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [ordi_ltqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [bachadate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT (getdate()) FOR [recv_dt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [hdate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [htime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [vgbel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [vsbed];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [soposnr];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [ordi_size];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [vol];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wunit];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [vunit];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [pstyv];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [city];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [route];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [cretim];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [cust];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [deltypdesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [custpo];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [pstyvdesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [rmrk];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [cmmt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [cust_name1];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [deltyp];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [cust_name2];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [routedesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [post];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [sono];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [street];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [tel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wecity];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [nwgt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [gwgt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [plant];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [matnr];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [matnrdesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [lgort];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [charg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [print_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [ordi_seq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [ordi_check];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [shipno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [sodate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [custpodate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [rqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [fqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [car_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [car_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ((0)) FOR [car_sno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [contry];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wecust_name1];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [region];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wecust_name2];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wecust];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [westreet];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wepost];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wetel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [wecontry];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [weregion];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [duedate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordi]
    ADD DEFAULT ('') FOR [parcel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordx]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordx]
    ADD DEFAULT ('') FOR [idate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordx]
    ADD DEFAULT ('') FOR [oprod];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordx]
    ADD DEFAULT ('') FOR [cretim];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordx]
    ADD DEFAULT ('') FOR [credat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordx]
    ADD DEFAULT ('') FOR [itime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiordx]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmto]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmto]
    ADD DEFAULT ('0') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ((0)) FOR [qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ('') FOR [credat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ('') FOR [idate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ('') FOR [oprod];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ('') FOR [cretim];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ('') FOR [itime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[hiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[hiwmtx]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mibacha]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mibacha]
    ADD DEFAULT ((1)) FOR [Sno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[midest]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[midest]
    ADD DEFAULT ('') FOR [area_code];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mijchg]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mijchg]
    ADD DEFAULT ('0') FOR [plti_label];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mijchg]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mijchg]
    ADD DEFAULT ('') FOR [plti_bestq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_desc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_type];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_grp];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_old];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_bunit];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_szdm];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ((0)) FOR [mast_gwgt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ((0)) FOR [mast_nwgt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_vunit];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ((0)) FOR [mast_vol];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_wunit];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('0') FOR [mast_flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_desc1];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_time];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ('') FOR [mast_date];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimast]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimast]
    ADD DEFAULT ((1)) FOR [mast_canqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[mimvht]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[mimvht]
    ADD DEFAULT ((0)) FOR [mvht_ioqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [parcel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [matnrdesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [matnr];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wecontry];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wetel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [weregion];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [ordi_size];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [duedate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [plant];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [lgort];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [charg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [pstyv];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [vunit];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [vol];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wunit];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [gwgt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [nwgt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [cust];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [sodate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [soposnr];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [fqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [rqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [custpodate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [car_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [cretim];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [route];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [deltyp];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [credat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [westreet];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wecust_name2];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wecust_name1];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wecity];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wecust];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [region];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [contry];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [shipno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [post];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [city];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [tel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [wepost];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [htime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [street];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [hdate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [pstyvdesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [rmrk];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [vgbel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [routedesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [vsbed];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [sono];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [ordi_seq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [ablad];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [ordi_check];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [arrival];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [print_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [car_sno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [bachadate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [cmmt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ((0)) FOR [ordi_ltqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [cust_name2];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [cust_name1];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [car_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [custpo];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT (getdate()) FOR [recv_dt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miordi]
    ADD DEFAULT ('') FOR [deltypdesc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miplti]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miplti]
    ADD DEFAULT ('0') FOR [plti_label];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miplti]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miplti]
    ADD DEFAULT ('') FOR [plti_bestq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miuser]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miuser]
    ADD DEFAULT ('') FOR [passwd];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miuser]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miuser]
    ADD DEFAULT ('') FOR [username];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miuser]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miuser]
    ADD DEFAULT ('') FOR [role];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miuser]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miuser]
    ADD DEFAULT (getdate()) FOR [credt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miwmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miwmto]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[miwmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[miwmto]
    ADD DEFAULT ('0') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [hTime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [hdate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('0') FOR [parcel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ((0)) FOR [seq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [bachadate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [area_code];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ((200)) FOR [priority];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('V') FOR [vol_qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('0') FOR [uuse];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('0') FOR [step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ((0)) FOR [load_qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ((0)) FOR [max_qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [dueDate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [car_dest];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [car_man];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [dueTime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [car_desc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ((0)) FOR [max_vol];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tacar]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tacar]
    ADD DEFAULT ((0)) FOR [load_vol];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [hdate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [bachadate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT (getdate()) FOR [recv_dt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ((0)) FOR [ordi_ltqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [shipno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [ordi_check];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [htime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [vgbel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [vsbed];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [parcel];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ((0)) FOR [rqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ((0)) FOR [fqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ((0)) FOR [ordi_seq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [cmmt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('0') FOR [car_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [rmrk];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ((0)) FOR [car_sno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('0') FOR [print_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [arrival];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [car_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ('') FOR [ablad];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[taordi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[taordi]
    ADD DEFAULT ((0)) FOR [ordi_size];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [bachadate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [bigo];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT (getdate()) FOR [recv_dt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [ordi_check];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('0') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [car_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [car_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ((0)) FOR [car_sno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [arrival];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ((0)) FOR [ordi_seq];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ('') FOR [print_step];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ((0)) FOR [ordi_size];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ((0)) FOR [fqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ((0)) FOR [rqty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tawmto]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tawmto]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbberr]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbberr]
    ADD DEFAULT ('') FOR [err_mmsg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbberr]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbberr]
    ADD DEFAULT ('') FOR [err_act];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbberr]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbberr]
    ADD DEFAULT ('') FOR [err_msg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbberr]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbberr]
    ADD DEFAULT ('') FOR [err_pltno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbbprn]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbbprn]
    ADD DEFAULT ('1') FOR [prn_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbbprn]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbbprn]
    ADD DEFAULT ((1)) FOR [prn_mixcnt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbbprn]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbbprn]
    ADD DEFAULT ('0') FOR [prn_flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbbprn]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbbprn]
    ADD DEFAULT (getdate()) FOR [prn_date];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0000000000000000') FOR [cnvc_ch04];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0000000000000000') FOR [cnvc_ch01];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0000000000000000') FOR [cnvc_ch03];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0000000000000000') FOR [cnvc_ch02];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0000000000000000') FOR [cnvc_ch05];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0000000000000000') FOR [cnvc_ch06];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000000') FOR [cnvc_op_onof];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000000') FOR [cnvc_op_eror];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('000000000000000000000000000000000000000000000000000000000000') FOR [cnvc_job_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000') FOR [cnvc_jobno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000000000000000000000000000000000000000000000000') FOR [cnvc_buf_palt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000') FOR [cnvc_ist_redy];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000') FOR [cnvc_ist_palt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000') FOR [cnvc_ost_redy];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('00000') FOR [cnvc_ost_palt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0') FOR [cnvc_21_rqst];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0') FOR [cnvc_22_rqst];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('1') FOR [cnvc_remote];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0') FOR [cnvc_stop];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0') FOR [cnvc_comm];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbcnvc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbcnvc]
    ADD DEFAULT ('0') FOR [cnvc_24_rqst];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_wdate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('W') FOR [evnt_sflg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_xmov];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('W') FOR [evnt_uflg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('S') FOR [evnt_wflg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_jio];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('A') FOR [evnt_gubn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_hogi];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_fstn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_pltn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_lstk];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbevnt]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbevnt]
    ADD DEFAULT ('') FOR [evnt_tstn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbhogi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbhogi]
    ADD DEFAULT ((0)) FOR [hogi_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbhogi]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbhogi]
    ADD DEFAULT ((2)) FOR [hogi_no2];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_hogi];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_gubn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_indx];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_tstn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_jio];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_fstn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_lstk];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_pltn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_xmov];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('') FOR [indx_edat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('W') FOR [indx_sflg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbindx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbindx]
    ADD DEFAULT ('0') FOR [indx_uflg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tblock]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tblock]
    ADD DEFAULT ((0)) FOR [lock_proc];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscer]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscer]
    ADD DEFAULT ('') FOR [scer_mesg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('I3') FOR [scrc_mode];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_gubn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_io];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_rset];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_comm];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_fstn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_indx];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_tstn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_xmov];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_mesg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_chdt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_jno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_pltn];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_lstk];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('1') FOR [scrc_ouse];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('1') FOR [scrc_iuse];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_eror];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_stop];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('') FOR [scrc_ecod];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0000') FOR [scrc_posi];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_palt];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0001') FOR [scrc_stat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_emer];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_onln];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbscrc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbscrc]
    ADD DEFAULT ('0') FOR [scrc_pwron];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbseqn]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbseqn]
    ADD DEFAULT ((0)) FOR [seqn_no];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbstat]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbstat]
    ADD DEFAULT ('0') FOR [stat_imode];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbstat]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbstat]
    ADD DEFAULT ('0') FOR [stat_ipath];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbstat]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbstat]
    ADD DEFAULT ('0') FOR [stat_resp_load];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tbstat]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tbstat]
    ADD DEFAULT ('0') FOR [stat_auto_load];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tibarc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tibarc]
    ADD DEFAULT ('') FOR [barc_pltno];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tibarc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tibarc]
    ADD DEFAULT ('') FOR [barc_msg];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tibarc]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tibarc]
    ADD DEFAULT (getdate()) FOR [barc_date];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiordx]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiordx]
    ADD DEFAULT ('') FOR [credat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiordx]
    ADD DEFAULT ('') FOR [cretim];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiordx]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiordx]
    ADD DEFAULT ('') FOR [itime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiordx]
    ADD DEFAULT ('') FOR [idate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiordx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiordx]
    ADD DEFAULT ('') FOR [oprod];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ('') FOR [remark];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ('') FOR [cretim];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ('') FOR [credat];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ('') FOR [oprod];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ('') FOR [idate];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ('') FOR [flag];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ((0)) FOR [qty];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ('') FOR [itime];


GO
PRINT N'DEFAULT 제약 조건 [dbo].[tiwmtx]에 대한 명명되지 않은 제약 조건을(를) 만드는 중...';


GO
ALTER TABLE [dbo].[tiwmtx]
    ADD DEFAULT ((0)) FOR [pksz];


GO
PRINT N'트리거 [dbo].[trg_miordi]을(를) 만드는 중...';


GO
CREATE TRIGGER [dbo].[trg_miordi]
	ON [dbo].[miordi]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		declare @arrival varchar(MAX) = ''
		declare @area_code varchar(20) = ''
		declare @ordi_size decimal = 0
		declare @ordi_ltqty decimal = 0

		select @arrival = westreet from inserted
		select @area_code = weregion from inserted
		
		update midest set area_code = @area_code where arrival = @arrival
		if @@ROWCOUNT = 0 begin
			begin try
				insert into midest( arrival, area_code) values (@arrival, @area_code)
			end try
			begin catch
			end catch
		end
		
		declare @qty decimal = 0
		declare @lgort varchar(4)
		declare @charg varchar(10)
		
		select @qty = qty, @lgort = lgort, @charg = charg from inserted;
		if @qty = 0 return
		if @lgort = '' return
		if @charg = '0' or @charg = '' return

		if @qty > 0 begin
			begin try
				select @ordi_size = vol / qty, @ordi_ltqty = vol, @arrival = westreet from inserted		
			end try
			begin catch
				select  @ordi_ltqty = vol,  @arrival = westreet from inserted
			end catch		
		end
		
		INSERT INTO taordi  select * from inserted

	END
GO
PRINT N'트리거 [dbo].[Trigger_miordi]을(를) 만드는 중...';


GO
CREATE TRIGGER [Trigger_miordi]
	ON [dbo].[miordi]
	for UPDATE 
		AS
	BEGIN
		SET NOCOUNT ON

		if update(rqty) begin
			declare @qt decimal
			declare @rq decimal

			select @qt = qty, @rq = rqty from inserted			

			if @rq < 0 begin
				update p set p.rqty = 0 from miordi p inner join inserted i 
				on p.docnum = i.docnum 
				   and p.sdno = i.sdno 
				   and p.posnr = i.posnr 
				   and p.ordi_seq = i.ordi_seq 
			end
			else if @rq > @qt begin
				update p set p.rqty = @qt from miordi p inner join inserted i 
				on p.docnum = i.docnum 
				   and p.sdno = i.sdno 
				   and p.posnr = i.posnr 
				   and p.ordi_seq = i.ordi_seq 
			end
		end	 

		if update(fqty) begin
			declare @fq decimal
			select @fq = fqty from inserted			

			if @fq < 0 begin
				update p set p.fqty = 0 from miordi p inner join inserted i 
				on p.docnum = i.docnum 
					and p.sdno = i.sdno 
					and p.posnr = i.posnr 
					and p.ordi_seq = i.ordi_seq 
			end
		end
	END
GO
PRINT N'트리거 [dbo].[Trg_miplti]을(를) 만드는 중...';


GO
create trigger [Trg_miplti]
	ON [dbo].[miplti]
	after UPDATE 
	AS
	begin
		set nocount on
	    declare @rq decimal

		select @rq = plti_rqty from inserted

		if @rq < 0 begin
			update p set p.plti_rqty = 0 from miplti p inner join inserted i 
			on p.plti_pltno = i.plti_pltno 
			   and p.plti_lstk = i.plti_lstk 
			   and p.plti_prod = i.plti_prod 
			   and p.plti_loc = i.plti_loc 
			   and p.plti_lot = i.plti_lot 
			   and p.plti_bestq = i.plti_bestq
		end

	end
GO
PRINT N'트리거 [dbo].[trg_miwmto]을(를) 만드는 중...';


GO
CREATE TRIGGER [dbo].[trg_miwmto]
	ON [dbo].[miwmto]
	AFTER INSERT
	AS
	BEGIN
		SET NOCOUNT ON
		declare @io varchar(1)
		select @io = io from inserted

		if (@io = '$') begin
			INSERT INTO tawmto  
			 ( docnum,   
			   credat,   
			   cretim,   
			   lgnum,   
			   tanum,   
			   bwlvs,   
			   trart,   
			   bname,   
			   tapos,   
			   matnr,   
			   plant,   
			   charg,   
			   bestq,   
			   sobkz,   
			   lsonr,   
			   meins,   
			   wdatu,   
			   wenum,   
			   vltyp,   
			   vsolm,   
			   nltyp,   
			   maktx,   
			   vfdat,   
			   lgort,   
			   io,   
			   rqty,   
			   fqty,   
			   flag,   
			   hdate,   
			   htime,   
			   pksz) 	
			 select docnum,   
			   credat,   
			   cretim,   
			   lgnum,   
			   tanum,   
			   bwlvs,   
			   trart,   
			   bname,   
			   tapos,   
			   matnr,   
			   plant,   
			   charg,   
			   bestq,   
			   sobkz,   
			   lsonr,   
			   meins,   
			   wdatu,   
			   wenum,   
			   vltyp,   
			   vsolm,   
			   nltyp,   
			   maktx,   
			   vfdat,   
			   lgort,   
			   io,   
			   rqty,   
			   fqty,   
			   flag,   
			   hdate,   
			   htime,   
			   pksz   
			   from inserted
		end
	END
GO
PRINT N'트리거 [dbo].[Trigger_miwmto]을(를) 만드는 중...';


GO
CREATE TRIGGER [Trigger_miwmto]
	ON [dbo].[miwmto]
	FOR UPDATE
	AS
	BEGIN
		SET NOCOUNT ON

		if update(rqty) begin
			declare @qt decimal
			declare @rq decimal

			select @qt = vsolm, @rq = rqty from inserted

			if @rq < 0 begin
				update p set p.rqty = 0 from miwmto p inner join inserted i 
				on p.docnum = i.docnum 
				   and p.tanum = i.tanum 
				   and p.tapos = i.tapos 
			end
			else if @rq > @qt begin
				update p set p.rqty = @qt from miwmto p inner join inserted i 
				on p.docnum = i.docnum 
				   and p.tanum = i.tanum 
				   and p.tapos = i.tapos 
			end
		end

		if update(fqty) begin
			declare @fq decimal

			select @fq = fqty from inserted

			if @fq < 0 begin
				update p set p.fqty = 0 from miwmto p inner join inserted i 
				on p.docnum = i.docnum 
				   and p.tanum = i.tanum 
				   and p.tapos = i.tapos 
			end
		end
	END
GO
PRINT N'뷰 [dbo].[v_etc_out]을(를) 만드는 중...';


GO

CREATE view [dbo].[v_etc_out]
as (
Select 3 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, lstk_hogi, plti_prod, plti_pdesc,  
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_lstk like  'A%'
and    lstk_stat = '10'
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag = '1' 
and    plti_bestq not in ('S', 'Q')
UNION
Select 2 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, '0' AS lstk_hogi, plti_prod, plti_pdesc, 
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_pltno <> '00000000'
and    plti_lstk like  'Y%'
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag = '1' 
and    plti_bestq not in ('S', 'Q')
UNION
Select 1 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, '0' AS lstk_hogi, plti_prod, plti_pdesc, 
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_pltno = '00000000'
and    plti_lstk like  'Y%'
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag  = '1' 
and    plti_bestq not in ('S', 'Q')
) ;
GO
PRINT N'뷰 [dbo].[v_exec_etc]을(를) 만드는 중...';


GO

CREATE view [dbo].[v_exec_etc]
as (
Select 3 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, lstk_hogi, plti_prod, plti_pdesc,  
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_lstk like  'A%'
and    lstk_stat = '10'
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag = '1' 
and    plti_bestq not in ('S', 'Q')
UNION
Select 2 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, '0' AS lstk_hogi, plti_prod, plti_pdesc, 
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_pltno <> '00000000'
and    plti_lstk like  'Y%'
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag = '1' 
and    plti_bestq not in ('S', 'Q')
UNION
Select 1 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, '0' AS lstk_hogi, plti_prod, plti_pdesc, 
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_pltno = '00000000'
and    plti_lstk like  'Y%'
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag  = '1' 
and    plti_bestq not in ('S', 'Q')
) ;
GO
PRINT N'뷰 [dbo].[v_rsrv]을(를) 만드는 중...';


GO

CREATE view [dbo].[v_rsrv]
as (
Select 3 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, lstk_hogi, plti_prod, plti_pdesc,  
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_lstk like  'A%'
and    lstk_stat in ('10', '$R')
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag in ('1', '$') 
UNION
Select 2 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, '0' AS lstk_hogi, plti_prod, plti_pdesc, 
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_pltno <> '00000000'
and    plti_lstk like  'Y%'
and    lstk_use  = '1'
and    plti_stok > 0 
and    plti_flag in ('1', '$') 
UNION
Select 1 as dumy, plti_pltno, plti_stok, plti_rqty, plti_lstk, plti_pksz, plti_cycl_date, '0' AS lstk_hogi, plti_prod, plti_pdesc, 
plti_loc, plti_lot, plti_bestq, plti_idate, plti_itime, plti_remark, plti_oprod
from miplti, milstk 
where  plti_lstk = lstk_no
and    plti_pltno = '00000000'
and    plti_lstk like  'Y%'
and    plti_stok > 0 
and    plti_flag in ('1', '$') 
) ;
GO
PRINT N'프로시저 [dbo].[p_creoldcode]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_creoldcode]	
AS
declare @prod varchar(18)
declare @vol decimal (13,3) 


	declare c1 cursor for 
	select a.plti_prod from miplti a inner join mimast b on a.plti_prod = b.mast_cd and a.plti_pksz <> b.mast_vol group by plti_prod; 

	begin transaction
	open c1;
	while 1 > 0 begin
		fetch c1 into @prod
		if @@FETCH_STATUS <> 0 break;

		select @vol = mast_vol from mimast where mast_cd = @prod;
		if @@ROWCOUNT > 0 begin
			update miplti set plti_pksz = @vol where plti_prod = @prod
		end
	end
	close c1;
	deallocate c1;
	commit;

RETURN 1
GO
PRINT N'프로시저 [dbo].[p_curgetdatetime10]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_curgetdatetime10]
	@dtstr varchar(10) output
AS
begin
	declare @ls varchar(19) = '';	
	select @ls = convert(varchar(19), getdate(), 121) from tbstat ;
	set @dtstr = substring(@ls, 1,4) + '/' + substring(@ls, 6,2) + '/' + substring(@ls, 9,2) ;
	 
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_curgetdatetime14]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_curgetdatetime14]
	@dtstr varchar(14) output
AS
begin
	declare @ls varchar(19) = '';	
	select @ls = convert(varchar(19), getdate(), 121) from tbstat ;
	set @dtstr = substring(@ls, 1,4) + substring(@ls, 6,2) + substring(@ls, 9,2) + substring(@ls, 12,2) + substring(@ls, 15,2) + substring(@ls, 18,2) ;
	 
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_curgetdatetime19]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_curgetdatetime19]
@dtstr varchar(19) output
AS
begin
	declare @ls varchar(19) = '';
	select @ls = convert(varchar(19), getdate(), 121) from tbstat	;
	select @dtstr = substring(@ls, 1,4) + '/' + substring(@ls, 6,2) + '/' + substring(@ls, 9,2) +  ' ' + substring(@ls, 12,2) + ':' + substring(@ls, 15,2) + ':' +  substring(@ls, 18,2) ;
	 
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_deplt]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_deplt]
	@pltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc  varchar(4),
	@lot  varchar(10),
	@bestq varchar(1),
	@stok decimal
AS
begin
	
--if igb = 'F' then
--	loca  = 'F000000'
--else
--	loca = 'Y000000'
--end if	

	
	declare @cc int = 0;
	declare @ls_type varchar(1);
	declare @ls_lstk varchar(6) = '';

	declare @dtstr varchar(19) = '';
	declare @idate varchar(10) = '';
	declare @itime varchar(8) = '';
	declare @lhno int = 0;
	declare @ls_hogi char(1);

	select @cc = count(*) from miplti 
	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq
	and plti_stok = @stok;
	
	if @cc = 0 return -1;  -- 상태변함

	select @cc = count(*) from miplti 
	where plti_pltno = @pltno and plti_lstk = @lstk and plti_rqty > 0;
	if @cc > 0 return -2;    -- 출고예약이 있음
	
	
	update miplti set plti_stok = plti_stok + @stok, plti_label = '0'
	where plti_pltno = '00000000'
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq;
	if @@ROWCOUNT = 0 begin
	
		INSERT INTO miplti  
  		      ( plti_pltno,      plti_lstk,   plti_prod,    plti_loc,   plti_lot,   
     		    plti_bestq,      plti_pksz,   plti_remark,  plti_stok,  plti_rqty, 
				plti_cycl_date,  plti_flag,   plti_idate,   plti_itime, plti_label,
				plti_oprod,      plti_pdesc,  plti_icust )  
		select  '00000000',      plti_lstk,   plti_prod,    plti_loc,   plti_lot,   
     		    plti_bestq,      plti_pksz,   plti_remark,  plti_stok,  0, 
				plti_cycl_date,  plti_flag,   plti_idate,   plti_itime, plti_label,
				plti_oprod,      plti_pdesc,  plti_icust  
		from miplti
		where plti_pltno = @pltno and plti_lstk = @lstk
		and plti_loc = @loc and plti_lot = @lot and plti_bestq = @bestq;

	end
	
	delete from miplti
	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_loc = @loc and plti_lot = @lot and plti_bestq = @bestq
	and plti_stok = @stok;
	if @@ROWCOUNT = 0 return -3;

	RETURN 1; 
end
GO
PRINT N'프로시저 [dbo].[p_deplt_n]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_deplt_n]
	@pltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc  varchar(4),
	@lot  varchar(10),
	@bestq varchar(1),
	@stok decimal,
	@sqty decimal
AS
begin
	
--if igb = 'F' then
--	loca  = 'F000000'
--else
--	loca = 'Y000000'
--end if	

	
	declare @cc int = 0;
	declare @ls_type varchar(1);
	declare @ls_lstk varchar(6) = '';

	declare @dtstr varchar(19) = '';
	declare @idate varchar(10) = '';
	declare @itime varchar(8) = '';
	declare @lhno int = 0;
	declare @ls_hogi char(1);

	select @cc = count(*) from miplti 
	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq
	and plti_stok = @stok;
	
	if @cc = 0 return -1;  -- 상태변함

	select @cc = count(*) from miplti 
	where plti_pltno = @pltno and plti_lstk = @lstk and plti_rqty > 0;
	if @cc > 0 return -2;    -- 출고예약이 있음
	
	
	update miplti set plti_stok = plti_stok + @sqty, plti_label = '0'
	where plti_pltno = '00000000'
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq;
	if @@ROWCOUNT = 0 begin
	
		INSERT INTO miplti  
  		      ( plti_pltno,      plti_lstk,   plti_prod,    plti_loc,   plti_lot,   
     		    plti_bestq,      plti_pksz,   plti_remark,  plti_stok,  plti_rqty, 
				plti_cycl_date,  plti_flag,   plti_idate,   plti_itime, plti_label,
				plti_oprod,      plti_pdesc,  plti_icust )  
		select  '00000000',      plti_lstk,   plti_prod,    plti_loc,   plti_lot,   
     		    plti_bestq,      plti_pksz,   plti_remark,  @sqty,      0, 
				plti_cycl_date,  plti_flag,   plti_idate,   plti_itime, plti_label,
				plti_oprod,      plti_pdesc,  plti_icust  
		from miplti
		where plti_pltno = @pltno and plti_lstk = @lstk
		and plti_loc = @loc and plti_lot = @lot and plti_bestq = @bestq;

	end
	
		
	update miplti set plti_stok = plti_stok - @sqty
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq;

	delete from miplti
	where plti_pltno = @pltno 
	and plti_lstk = @lstk
	and plti_loc = @loc 
	and plti_lot = @lot 
	and plti_bestq = @bestq
	and plti_stok = 0 
	and plti_rqty = 0;


	RETURN 1; 
end
GO
PRINT N'프로시저 [dbo].[p_etc_cnfm]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_etc_cnfm]
	@docnum varchar(16), 
	@tanum decimal,
	@tapos int
AS
begin
	
	delete from tiwmtx where docnum = @docnum and tanum = @tanum and tapos = @tapos;
	
	
	delete from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos and fqty >= vsolm; 

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_etc_rsrv_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_etc_rsrv_cancel]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@wmtxkey decimal,
	@pltno varchar(8),
	@lstk varchar(7),
	@oqty decimal

AS
begin

declare @lc int = 0;

	--exec p_tilock;
	declare @matnr varchar(18)
	declare @lgort varchar(4)
	declare @charg varchar(10)
	declare @bestq varchar(1)

	select @matnr = matnr, @lgort = lgort, @charg = charg ,  @bestq = bestq
	from miwmto
	 where docnum = @docnum and tanum = @tanum and tapos = @tapos;
	if @@ROWCOUNT = 0 return -1;  -- 상태변함 miwmto

	delete from tiwmtx where wmtxkey = @wmtxkey and flag = '$R';
	if @@ROWCOUNT = 0 return -2;  -- 상태변함 tiwmtx

	update miplti set plti_stok = plti_stok + @oqty, 
	                  plti_rqty = plti_rqty - @oqty
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
     and plti_prod = @matnr
	  and plti_loc = @lgort
	  and plti_lot = @charg
	  and plti_bestq = @bestq
	if @@ROWCOUNT = 0 return -3;  -- 상태변함 miplti

	if SUBSTRING(@lstk, 1,1) = 'A' begin
		select @lc =count(*) from tiwmtx where lstk = @lstk;
		if @lc = 0 begin
			update milstk set lstk_io = '0', lstk_stat = '10' 
			where lstk_no = @lstk
			  and 0 = (select count(*) from miplti 
				        where plti_lstk = @lstk
            		    and plti_rqty > 0 ) ;

		end
	end

	update miwmto set rqty = rqty - @oqty
	where  docnum = @docnum
	and    tanum  = @tanum
	and    tapos  = @tapos;
	if @@ROWCOUNT = 0 return -4;  -- 상태변함 miwmto

RETURN 1
end
GO
PRINT N'프로시저 [dbo].[p_etc_rsrv_uline2]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_etc_rsrv_uline2]  -- 실행시는 반드시 lstk로 sort바람
	@docnum varchar(16), 
	@tanum decimal, 
	@tapos int,
	@matnr varchar(18), 
	@lgort varchar(4), 
	@charg varchar(10),
	@bestq varchar(1),
	@oqty  decimal = 0 output
AS
begin
	declare @oq   decimal
	declare @rq decimal;
	declare @sq decimal;
	
	declare @date varchar(8);
	declare @time varchar(6);

	declare @canqty int = 1;
	
	
	declare @ho1 varchar(1) = '1';
	declare @ho2 varchar(1) = '2';
	declare @ho3 varchar(1) = '3';
	declare @ho4 varchar(1) = '4';
	declare @ho5 varchar(1) = '5';
	declare @scrc_gbun varchar(1);
	declare @scrc_onln varchar(1);
	declare @scrc_emer varchar(1);
	declare @scrc_ouse varchar(1);
	declare @scrc_comm varchar(1);

	declare @dumy int;
	declare @pltno varchar(8);
	declare @loca varchar(7);
	declare @pstok decimal;
	declare @prq decimal;
	declare @pksz decimal(18,3);
	declare @remark varchar(40);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @maktx varchar(40);
	declare @bwlvs varchar(1);
	declare @oprod varchar(18);

	declare @soqty decimal = 0;
	declare @fail int = 0
	set @oqty = 0;
	
	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '01';
	if @scrc_ouse = '0' set @ho1 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '02';
	if @scrc_ouse = '0' set @ho2 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '03';
	if @scrc_ouse = '0' set @ho3 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '04';
	if @scrc_ouse = '0' set @ho4 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '05';
	if @scrc_ouse = '0' set @ho5 = '9';
		 
	declare @rc int = 0;
	declare @lp int = 0;
	declare @dts varchar(14) = '';

	declare @odate varchar(8);
	declare @otime varchar(6);
	
	
	exec @rc = p_curgetdatetime14 @dts output;	
	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);

	-- lock ----
	--exec p_tilock;	
		
    SELECT @oq = vsolm, @rq = rqty, @bwlvs = bwlvs
      FROM miwmto   
	where docnum = @docnum
	  and tanum = @tanum
	  and tapos = @tapos
	  and matnr = @matnr
	  and lgort = @lgort
	  and charg = @charg
	  and bestq = @bestq
	  and io = '$'
	  and vsolm - rqty > 0 
	if @@ROWCOUNT = 0 return 0

	set @sq = @oq - @rq;		
	while @sq > 0 begin
				
		-- 같은수량 존재 확인
		Select top 1 
			@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
			@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
		from v_rsrv (updlock)
		where  plti_prod = @matnr
		and    plti_loc = @lgort
		and    plti_lot = @charg
		and    plti_bestq = @bestq
		and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' )
		and    plti_stok = @sq 
		order by 1, 3, 8, 9;
		if @@ROWCOUNT = 0 begin		
		
			Select top 1 
				@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
				@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
			from v_rsrv (updlock)
			where  plti_prod = @matnr
			and    plti_loc = @lgort
			and    plti_lot = @charg
			and    plti_bestq = @bestq
			and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' )
			and    plti_stok > 0 order by 1, 3, 8, 9;
			if @@ROWCOUNT = 0 begin					
				break;
			end		

			if @sq > @pstok  begin --large order so fetch again
				update miplti set plti_stok = plti_stok - @pstok, plti_rqty = plti_rqty + @pstok
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = @bestq;
				if @@ROWCOUNT = 0 begin 
					set @lp  = 0
					set @fail = 1
					break
				end
							
				set @soqty = @pstok;
				set @sq = @sq - @pstok;
			end
			else begin     -- large plti to scr again
				update miplti set plti_stok = plti_stok - @sq, plti_rqty = plti_rqty + @sq
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  =  @bestq;
				if @@ROWCOUNT = 0 begin 
					set @lp  = 0
					set @fail = 1
					break
				end
		
				set @soqty = @sq;
				set @sq = 0; 
			end
		end
		else begin -- 같은 수량
	
			set @soqty =  @sq;
			set @sq = 0;

			update miplti set plti_stok = plti_stok - @soqty, plti_rqty = plti_rqty + @soqty
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  =  @bestq;
			if @@ROWCOUNT = 0 begin 
				set @lp  = 0
				set @fail = 1
				break
			end

		end
		
		if @loca = null or  @pltno = null or  @oqty = null or @pksz = null begin
			set @fail = 1
			set @lp = 0
			break
		end

		if substring(@loca, 1, 1) = 'A' begin
			update milstk set lstk_io = '$', lstk_stat = '$R'  where lstk_no = @loca ;
		end
	
		INSERT INTO tiwmtx  
		 			( docnum,  tanum,  tapos,  lstk,   pltno,   qty,    flag,  pksz, credat,  cretim,   remark, idate,  itime,  oprod,   bwlvs,   IO )  
		    VALUES ( @docnum, @tanum, @tapos, @loca,  @pltno,  @soqty, '$R',  @pksz, @odate,  @otime,  @remark, @idate, @itime, @oprod,  @bwlvs,  '$' ) ;
		if @@ROWCOUNT = 0 begin 
			set @lp  = 0
			set @fail = 1
			break
		end

		set @oqty = @soqty + @oqty			
		set @lp = @lp + 1;

	end -- end while
		
	if @fail = 1 return 0

	if @oqty > 0 begin
		 update miwmto set rqty = rqty + @oqty
		 where docnum = @docnum
			and tanum = @tanum
			and tapos = @tapos
			and matnr = @matnr
			and lgort = @lgort
			and charg = @charg
			and bestq = @bestq
			and io = '$'
		if @@ROWCOUNT = 0 begin 
			set @lp = 0
			set @oqty = 0
		end
	end

	RETURN @lp;
end
GO
PRINT N'프로시저 [dbo].[p_fixpksz]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_fixpksz]	
AS
declare @prod varchar(18)
declare @vol decimal (13,3) 


	declare c1 cursor for 
	select a.plti_prod from miplti a inner join mimast b on a.plti_prod = b.mast_cd and a.plti_pksz <> b.mast_vol group by plti_prod; 

	begin transaction
	open c1;
	while 1 > 0 begin
		fetch c1 into @prod
		if @@FETCH_STATUS <> 0 break;

		select @vol = mast_vol from mimast where mast_cd = @prod;
		if @@ROWCOUNT > 0 begin
			update miplti set plti_pksz = @vol where plti_prod = @prod
		end
	end
	close c1;
	deallocate c1;
	commit;

RETURN 1
GO
PRINT N'프로시저 [dbo].[p_get_hogi]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_get_hogi]
	@lstk varchar(7),
	@hogi varchar(1) output
AS
begin
	
	declare @bk varchar(2);

	set @bk = substring(@lstk, 2,2);
	select @hogi = cast( (CONVERT(int, @bk) + 1) / 2 as varchar(1)) ;

	RETURN 1
end
GO
PRINT N'프로시저 [dbo].[p_get_indx_jno]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_get_indx_jno]
    @ac   varchar(1),
	@rstr varchar(18) output
AS
begin
	
	declare @ls varchar(14);
	declare @ls_date varchar(8);
	declare @indx varchar(4);
	declare @rc int = 0;
	
	exec @rc = p_curgetdatetime14 @ls output;

	if @ac = '1' begin   -- 공장입고
		select @ls_date = seqn_date from tbseqn where seqn_key = '1' 
		if SUBSTRING(@ls, 1,8) <> @ls_date begin
			set @ls_date = SUBSTRING(@ls, 1,8);
			update tbseqn 
			   set seqn_date = @ls_date,
   			       seqn_no = 0             
			where seqn_key = '1' ;
		end
		update tbseqn 
		   set seqn_date = @ls_date,
   		       seqn_no = seqn_no + 1 
		where seqn_key = '1';
		select @indx = seqn_no from tbseqn where seqn_key = '1' ;
	end
	if @ac = '2' begin   -- 메인입고
		select @ls_date = seqn_date from tbseqn where seqn_key = '2' 
		if SUBSTRING(@ls, 1,8) <> @ls_date begin
			set @ls_date = SUBSTRING(@ls, 1,8);
			update tbseqn 
			   set seqn_date = @ls_date,
   			       seqn_no = 2000             
			where seqn_key = '2' ;
		end
		update tbseqn 
		   set seqn_date = @ls_date,
   		       seqn_no = seqn_no + 1 
		where seqn_key = '2';
		select @indx = seqn_no from tbseqn where seqn_key = '2' ;
	end

	if @ac = '3' begin   -- 정상출고
		select @ls_date = seqn_date from tbseqn where seqn_key = '3' 
		if SUBSTRING(@ls, 1,8) <> @ls_date begin
			set @ls_date = SUBSTRING(@ls, 1,8);
			update tbseqn 
			   set seqn_date = @ls_date,
   			       seqn_no = 5000             
			where seqn_key = '3' ;
		end
		update tbseqn 
		   set seqn_date = @ls_date,
   		       seqn_no = seqn_no + 1 
		where seqn_key = '3';
		select @indx = seqn_no from tbseqn where seqn_key = '3' ;
	end

	if @ac = '4' begin   -- 기타
		select @ls_date = seqn_date from tbseqn where seqn_key = '4' 
		if SUBSTRING(@ls, 1,8) <> @ls_date begin
			set @ls_date = SUBSTRING(@ls, 1,8);
			update tbseqn 
			   set seqn_date = @ls_date,
   			       seqn_no = 9000             
			where seqn_key = '4' ;
		end
		update tbseqn 
		   set seqn_date = @ls_date,
   		       seqn_no = seqn_no + 1 
		where seqn_key = '4';
		select @indx = seqn_no from tbseqn where seqn_key = '4' ;
	end

	 set @rstr = substring(@ls,1,14) + right('0000' + @indx, 4); 
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_get_rsrv_hogi]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_get_rsrv_hogi]
	@atype varchar(1),
	@srch varchar(6)  output
AS
begin
	
	declare @lhno int = 0;
	declare @hogi varchar(1);
	declare @ls_hogi char(2);
	declare @bonof varchar(5);
	declare @beror varchar(5);
	declare @lc int = 0;
	declare @onln varchar(1);
	declare @stop varchar(1);
	declare @emer varchar(1);
	declare @comm varchar(1);
	declare @iuse varchar(1);
	declare @eror varchar(1);

	select @lhno = hogi_no from tbhogi ;

	select @hogi = @lhno;

	select  @bonof = cnvc_op_onof, @beror = cnvc_op_eror from tbcnvc where cnvc_mode = '01';

	set @srch = '';

	while (@lc < 5) begin
	    set @lc = @lc + 1;

		set @lhno = @lhno + 1;
		if @lhno > 5 set @lhno = 1;

		select @hogi = @lhno;
		
		--if substring(@bonof, @lhno, 1) = '0' or substring(@beror, @lhno, 1) = '1'  continue;
	
		set @ls_hogi = '0' + @hogi;
		
		select @onln = scrc_onln, @stop = scrc_stop, @emer = scrc_emer, @comm = scrc_comm, @iuse = scrc_iuse, @eror = scrc_eror 
		from tbscrc where scrc_no = @ls_hogi;
		if @onln <> '1' continue;
		if @stop <> '0' continue;
		if @comm <> '1' continue;
		if @emer <> '0' continue;
		if @iuse <> '1' continue;
		if @eror <> '0' continue;
		
		Select top 1  @srch = lstk_srch  from milstk 
		where lstk_hogi = @hogi
   		and lstk_io   = '0'
		and lstk_use  = '1'
   		and lstk_stat = '00'
		and lstk_type in ('0', '1')
		and lstk_no like 'A%' 
		order by lstk_type, lstk_srch;
		if @@ROWCOUNT <> 0 break;
	end
	if @srch = '' return 0;
	
	update tbhogi set hogi_no = @lhno where hogi_key = '1' ;
	set @srch = substring(@srch,5,2) + substring(@srch,3,2) + substring(@srch,1,2);

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_get_rsrv_hogi1]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_get_rsrv_hogi1]
	@atype varchar(1),
	@srch varchar(6) output 	
AS
begin
	
	declare @lhno int = 0;
	declare @hogi varchar(1);
	declare @ls_hogi varchar(2);
	declare @bonof varchar(5);
	declare @beror char(5);
	declare @lc int = 0;
	declare @onln varchar(1);
	declare @stop varchar(1);
	declare @emer varchar(1);
	declare @comm varchar(1);
	declare @iuse varchar(1);
	declare @eror varchar(1);

	select @lhno = hogi_no from tbhogi ;

	select @hogi = @lhno;

	select  @bonof = cnvc_op_onof, @beror = cnvc_op_eror from tbcnvc where cnvc_mode = '01';

	set @srch = '';

	while (@lc < 5) begin
	    set @lc = @lc + 1;

		set @lhno = @lhno + 1;
		if @lhno > 5 set @lhno = 1;

		select @hogi = @lhno;
		
		--if substring(@bonof, @lhno, 1) = '0' or substring(@beror, @lhno, 1) = '1'  continue;
	
		set @ls_hogi = '0' + @hogi;

		select @onln = scrc_onln, @stop = scrc_stop, @emer = scrc_emer, @comm = scrc_comm, @iuse = scrc_iuse, @eror = scrc_eror 
		from tbscrc where scrc_no = @ls_hogi;
		if @onln <> '1' continue;
		if @stop <> '0' continue;
		if @comm <> '1' continue;
		if @emer <> '0' continue;
		if @iuse <> '1' continue;
		if @eror <> '0' continue;

		Select top 1  @srch = lstk_srch  from milstk 
		where lstk_hogi = @hogi
   		and lstk_io   = '0'
		and lstk_use  = '1'
   		and lstk_stat = '00'
		and lstk_type = '1'
		and lstk_lv = '01'
		and lstk_no like 'A%' 
		order by lstk_srch;
		if @@ROWCOUNT <> 0 break;
	end
	if @srch = '' return 0;
	
	update tbhogi set hogi_no = @lhno where hogi_key = '1' ;
	set @srch = substring(@srch,5,2) + substring(@srch,3,2) + substring(@srch,1,2);
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_get_rsrv_hogi2]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_get_rsrv_hogi2]
	@atype varchar(1),
	@srch varchar(6) output 
	
AS
begin
	--//-      Thinner 인 경우 02 - 05열 17번지까지 1 - 2단에 적재함. 
    --//-      Thinner 외 위험물은 Thinner 공간을 제외한 01 ? 10열 36번지까지 1단에 적재함.
	declare @lhno int = 0;
	declare @hogi varchar(1);
	declare @bonof varchar(5);
	declare @beror varchar(5);
	declare @lc int = 0;
	declare @onln varchar(1);
	declare @stop varchar(1);
	declare @emer varchar(1);
	declare @comm varchar(1);
	declare @iuse varchar(1);
	declare @eror varchar(1);
	declare @ls_bk varchar(2);
	declare @ls_hogi varchar(2);

	select @lhno = hogi_no2 from tbhogi ;
	select @hogi = @lhno;
	set @ls_bk = '0' + @hogi;
	

	select  @bonof = cnvc_op_onof, @beror = cnvc_op_eror from tbcnvc where cnvc_mode = '01';

	set @srch = '';

	while (@lc < 4) begin
	    set @lc = @lc + 1;

		set @lhno = @lhno + 1;
		if @lhno > 5 or @lhno < 2 set @lhno = 2;

		select @hogi = @lhno;
		set @ls_bk = '0' + @hogi;
		
		set @ls_hogi = '02';
		if @ls_bk = '02' set @ls_hogi = '01';
		if @ls_bk = '03' set @ls_hogi = '02';
		if @ls_bk = '04' set @ls_hogi = '03';

		--if substring(@bonof, @lhno, 1) = '0' or substring(@beror, @lhno, 1) = '1'  continue;
		
		select @onln = scrc_onln, @stop = scrc_stop, @emer = scrc_emer, @comm = scrc_comm, @iuse = scrc_iuse, @eror = scrc_eror 
		from tbscrc where scrc_no = @ls_hogi;
		if @onln <> '1' continue;
		if @stop <> '0' continue;
		if @comm <> '1' continue;
		if @emer <> '0' continue;
		if @iuse <> '1' continue;
		if @eror <> '0' continue;

		Select top 1  @srch = lstk_srch  from milstk 
		where lstk_bk = @ls_bk
   		and lstk_io   = '0'
		and lstk_use  = '1'
   		and lstk_stat = '00'
		and lstk_type = @atype
		and lstk_lv in ('01', '02')
		and lstk_no like 'A%' 	order by lstk_srch;
		if @@ROWCOUNT <> 0 break;
	end
	if @srch = '' return 0;
	
	update tbhogi set hogi_no2 = @lhno where hogi_key = '1' ;
	set @srch = substring(@srch,5,2) + substring(@srch,3,2) + substring(@srch,1,2);

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_get_rsrv_hogi3]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_get_rsrv_hogi3]
	@ghogi3 int = 4,
	@srch varchar(6) output 
	
AS
begin
	--//-      Thinner 인 경우 02 - 05열 17번지까지 1 - 2단에 적재함. 
    --//-      Thinner 외 위험물은 Thinner 공간을 제외한 01 ? 10열 36번지까지 1단에 적재함.
	declare @lhno int = 0;
	declare @hogi char(1);
	declare @bonof varchar(5);
	declare @beror varchar(5);
	declare @lc int = 0;
	declare @onln varchar(1);
	declare @stop varchar(1);
	declare @emer varchar(1);
	declare @comm varchar(1);
	declare @iuse varchar(1);
	declare @eror varchar(1);
	declare @ls_hogi varchar(2);

	set @lhno = @ghogi3;
	select @hogi = @lhno;

	select  @bonof = cnvc_op_onof, @beror = cnvc_op_eror from tbcnvc where cnvc_mode = '01';

	set @srch = '';

	while (@lc < 2) begin
	    set @lc = @lc + 1;

		set @lhno = @lhno + 1;
		if @lhno > 5 set @lhno = 4;
		select @hogi = @lhno;
			
		--if substring(@bonof, @lhno, 1) = '0' or substring(@beror, @lhno, 1) = '1'  continue;
		set @ls_hogi = '0' + @hogi;

		select @onln = scrc_onln, @stop = scrc_stop, @emer = scrc_emer, @comm = scrc_comm, @iuse = scrc_iuse, @eror = scrc_eror 
		from tbscrc where scrc_no = @ls_hogi;
		if @onln <> '1' continue;
		if @stop <> '0' continue;
		if @comm <> '1' continue;
		if @emer <> '0' continue;
		if @iuse <> '1' continue;
		if @eror <> '0' continue;

		Select top 1  @srch = lstk_srch  from milstk 
		where lstk_hogi = @hogi
   		and lstk_io   = '0'
		and lstk_use  = '1'
   		and lstk_stat = '00'
		and lstk_type ='3'
		and lstk_lv in ('01', '02')
		and lstk_no like 'A%' 	order by lstk_srch;
		if @@ROWCOUNT <> 0 break;
	end
	if @srch = '' return 0;
	set @srch = substring(@srch,5,2) + substring(@srch,3,2) + substring(@srch,1,2);

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_getpltno]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_getpltno]
	@pltno  varchar(8) output	
AS
begin
	declare @pp decimal = 0;
	declare @ps varchar(8) = '';

	select @pp = pltno from tipltn;
    
	if @pp >= 99999999 set @pp = 0;
	set @pp = @pp + 1;

	select @pltno = RIGHT('00000000' +  convert(varchar(8), @pp), 8);

	update tipltn set pltno = @pltno; 
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_getrand]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_getrand]
AS
	declare @ret int = 0
	select @ret = floor(rand() * 2000 + 1000) from tbstat
RETURN @ret
GO
PRINT N'프로시저 [dbo].[p_labelprn]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_labelprn]
	@lstk varchar(7),
	@pltno varchar(8),
    @prn   varchar(1)
AS
begin

	declare @stok decimal = 0;
	declare @prod varchar(18);
	declare @pdesc varchar(40);
	declare @lot varchar(10);
	declare @bestq varchar(1);
	declare @prn_no char(1);
	declare @pltcnt int =0;
	declare @pksz decimal(18,3);
	
	select @pltcnt = count(*), @stok = sum(plti_stok) from miplti where plti_pltno = @pltno;
	if @@ROWCOUNT = 0 return -1;  -- 없음 상태변함

	update miplti set plti_label = '1' where plti_pltno = @pltno;
	if @@ROWCOUNT = 0 return -2;  -- 없음 상태변함

	begin try
		if @pltcnt = 1 begin
			select top 1 @prod = plti_prod, @pdesc = plti_pdesc, @lot = plti_lot, @pksz = plti_pksz from miplti where plti_pltno = @pltno;
			if @@ROWCOUNT = 0 return -3;  -- 없음 상태변함

			INSERT INTO tbbprn  
			  		  ( prn_no,    prn_pltno,     prn_prod,  prn_pdesc,   prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
			 VALUES (   @prn,      @pltno,        @prod,     @pdesc,      @lot  ,     @pksz,     @stok,     1,            getdate()) ;

		end else begin

			INSERT INTO tbbprn  
			  		  ( prn_no,    prn_pltno,     prn_prod,  prn_pdesc,   prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
			 VALUES (   @prn,      @pltno,        '',        '',          ''  ,      0.00,       @stok,     @pltcnt,      getdate()) ;
		end
		

	end try
	begin catch
		return -99; -- 중복발행
	end catch;

	RETURN 1;

end
GO
PRINT N'프로시저 [dbo].[P_miwmto_in]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[P_miwmto_in]
	@docnum varchar(16),
	@tanum  decimal,
	@tapos  int
	
AS
begin
	declare @hdt varchar(19);
	declare @idate varchar(10);
	declare @itime varchar(8);

	declare @hdate varchar(8);
	declare @htime varchar(6);

	declare @prod   varchar(18);
	declare @oprod   varchar(18);
	declare @pdesc  varchar(40);
	declare @loc    varchar(4);
	declare	@lot    varchar(10);
	declare @stok   decimal;
	declare @pksz   decimal(18,3);
	declare @pksz2   decimal(18,3);
	declare @bwlvs varchar(3);
	declare @loca varchar(7);

	begin try

		select @prod = matnr, @pdesc =maktx, @loc = lgort, @lot = charg, @stok = vsolm, @bwlvs = bwlvs, @pksz = pksz 
		from miwmto  
		where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		if @@ROWCOUNT = 0 return -1;

		insert into hiwmto select * from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		if @@ROWCOUNT = 0 return -2;
		
		delete from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		if @@ROWCOUNT = 0 return -3;

		select @hdt = convert(char(19), getdate(), 121) from tbstat;
		select @hdate = substring(@hdt, 1,4)  + substring(@hdt, 6,2) +  substring(@hdt, 9,2);
		select @htime = substring(@hdt, 12,2) +  substring(@hdt, 15,2) +  substring(@hdt, 18,2);
	
		update hiwmto set hdate = @hdate, htime = @htime where docnum = @docnum and tanum = @tanum and tapos = @tapos;

		select @idate = substring(@hdt, 1,4) + '/' +substring(@hdt, 6,2) + '/' + substring(@hdt, 9,2);
		select @itime = substring(@hdt, 12,2) + ':' + substring(@hdt, 15,2) + ':' + substring(@hdt, 18,2);
		
		--if @bwlvs = '101' set @loca = 'F000000'
		--else  
		
		if (@pksz = 0) begin
			select @pksz = mast_vol from mimast where mast_cd = @prod;
			if @@ROWCOUNT = 0 begin
				RETURN -4;
			end
		end

		set @loca = 'Y000000'

		update miplti set plti_stok = plti_stok + @stok 
		where plti_pltno = '00000000' 
		  and plti_lstk = @loca
		  and plti_prod = @prod 
		  and plti_loc = @loc
		  and plti_lot = @lot
		  and plti_bestq = '';
		if @@ROWCOUNT = 0 begin	
			insert into miplti (plti_pltno, plti_lstk,  plti_prod,   plti_pdesc,     plti_loc,   plti_lot,  plti_bestq, 
								plti_pksz,  plti_stok,  plti_rqty,   plti_cycl_date, plti_idate, plti_itime, plti_remark, 
								plti_flag,  plti_icust, plti_label,  plti_oprod)
			values (           '00000000',  @loca,      @prod,       @pdesc,         @loc,       @lot,       '',
								@pksz,      @stok,      0,           @idate,         @idate,     @itime,     '',
								'1',        '',        '0',          '' );
		end
	end try
	begin catch
		return -99;
	end catch					          
	  	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_out_cnfm_all]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_cnfm_all]
	@credat varchar(8)	
AS
begin
		
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	
	declare @lp int = 0 ;

	declare @hdate varchar(8);
	declare @htime varchar(6);
	declare @dts varchar(14);

	exec p_curgetdatetime14 @dts output;
	set @hdate = SUBSTRING(@dts, 1, 8);
	set @htime = SUBSTRING(@dts, 9, 6);
	
	declare c1 cursor for
	select docnum, sdno, posnr  from miordi	where credat = @credat	and qty >= fqty;

	open c1;

	while(1 > 0) begin

		fetch c1 into @docnum, @sdno, @posnr;
		if @@FETCH_STATUS <> 0 break;
					
		delete from tiordx where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		
		delete from miordi where docnum = @docnum and sdno = @sdno and posnr = @posnr and  fqty >= qty;
		
		set @lp = @lp + 1;

	end
	close c1;
	DEALLOCATE C1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_out_cnfm_all_cust]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_cnfm_all_cust]
	@credat varchar(8),
	@cust varchar(17)	
AS
begin
		
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	
	declare @lp int = 0 ;

	declare @hdate varchar(8);
	declare @htime varchar(6);
	declare @dts varchar(14);

	exec p_curgetdatetime14 @dts output;
	set @hdate = SUBSTRING(@dts, 1, 8);
	set @htime = SUBSTRING(@dts, 9, 6);
	
	declare c1 cursor for
	select docnum, sdno, posnr  from miordi	where credat = @credat	and cust = @cust and qty >= fqty;

	open c1;

	while(1>0) begin

		fetch c1 into @docnum, @sdno, @posnr;
		if @@FETCH_STATUS <> 0 break;
					
		delete from tiordx where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		
		delete from miordi where docnum = @docnum and sdno = @sdno and posnr = @posnr and  fqty >= qty;
		
	    set @lp = @lp + 1;

	end
	close c1;
	deallocate c1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_out_cnfm_all_date]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_cnfm_all_date]
	@credat varchar(8)	
AS
begin
		
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	
	declare @lp int = 0 ;

	declare @hdate varchar(8);
	declare @htime varchar(6);
	declare @dts varchar(14);

	exec p_curgetdatetime14 @dts output;
	set @hdate = SUBSTRING(@dts, 1, 8);
	set @htime = SUBSTRING(@dts, 9, 6);
	
	declare c1 cursor for
	select docnum, sdno, posnr from miordi	where credat = @credat	and qty >= fqty;

	open c1;

	while(1>0) begin

		fetch c1 into @docnum, @sdno, @posnr;
		if @@FETCH_STATUS = 100 break;

		fetch c1 into @docnum, @sdno, @posnr;
		if @@FETCH_STATUS <> 0 break;
					
		delete from tiordx where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		
		delete from miordi where docnum = @docnum and sdno = @sdno and posnr = @posnr and  fqty >= qty;
	
		set @lp = @lp + 1;

	end
	close c1;
	deallocate c1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_out_cnfm_all_order]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_cnfm_all_order]
	@credat varchar(8),
	@orderno varchar(10)	
AS
begin
		
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	
	declare @lp int = 0 ;

	declare @hdate varchar(8);
	declare @htime varchar(6);
	declare @dts varchar(14);

	exec p_curgetdatetime14 @dts output;
	set @hdate = SUBSTRING(@dts, 1, 8);
	set @htime = SUBSTRING(@dts, 9, 6);
	
	declare c1 cursor for
	select docnum, sdno, posnr  from miordi	where credat = @credat	and sdno = @orderno and qty >= fqty;

	open c1;

	while(1>0) begin

		fetch c1 into @docnum, @sdno, @posnr;
		if @@FETCH_STATUS <> 0 break;
					
		delete from tiordx where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		
		delete from miordi where docnum = @docnum and sdno = @sdno and posnr = @posnr and  fqty >= qty;
	
		set @lp = @lp + 1;

	end
	close c1;
	deallocate c1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_out_exec]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_exec]-- 실행시는 반드시 lstk로 sort바람
	@credat varchar(8) -- delete
AS
begin
	
	declare @ordxkey decimal;
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	declare @pltno varchar(8);
	declare @lstk varchar(7);
	declare @oqty decimal;
	declare @matnr varchar(18)
	declare @matnrdesc varchar(40)
	declare @remark varchar(40)

	declare @lgort varchar(4);
	declare @charg varchar(10);
	declare @pksz decimal(13,3);

	declare @plstk varchar(7) = '';
	declare @flag varchar(2) = '';
	declare @jno varchar(18) = '';
	declare @rc int = 0 ;
	declare @indx varchar(4) ;
	declare @hogi varchar(1) ;
	declare @fstn varchar(2) ;
	declare @tstn varchar(2) ;
	declare @lp int = 0 ;

	declare @dts varchar(14) = '' ;
	exec p_curgetdatetime14 @dts output;
	declare @hdate varchar(8) = substring( @dts, 1,8)
	declare @htime varchar(6) = substring( @dts, 9,6)

	declare @dts19 varchar(19)
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts19 output
	set @iodate =substring(@dts19, 1,10)
	set @iotime =substring(@dts19, 12,8)
	

	declare c1 cursor for
	select b.ordxkey, a.docnum, a.sdno, a.posnr, b.pltno, b.lstk, b.qty, a.matnr, a.lgort, a.charg, b.pksz, a.matnrdesc, b.remark
	  from miordi a, tiordx b
	where a.docnum = b.docnum
	and a.sdno = b.sdno
	and a.posnr = b.posnr
	and b.flag = '$R' order by b.lstk; 

	open c1;

	while(1>0) begin

		fetch c1 into @ordxkey, @docnum, @sdno, @posnr, @pltno, @lstk, @oqty, @matnr, @lgort, @charg, @pksz, @matnrdesc, @remark;
		if @@FETCH_STATUS <> 0 break;

		if substring(@lstk,1,1) = 'A' begin
			set @flag = '$X';			
			if (@lstk <> @plstk) begin
				update milstk set lstk_io = '$', lstk_stat = '$X'	where lstk_no = @lstk and lstk_stat = '$R'

				exec @rc = p_get_indx_jno '3',  @jno output
				set @indx = Right(@jno, 4);

				exec p_get_hogi @lstk, @hogi output;

				set @fstn = right('00' + cast(CONVERT(int, @hogi) * 2 as varchar(2)), 2);			
				set @tstn = '43';

				INSERT INTO tbindx  
  	      			  ( indx_jno,     indx_indx,       indx_gubn,        indx_jio,        indx_hogi,   
  		      			indx_fstn,    indx_tstn,       indx_pltn,        indx_lstk,       indx_xmov,   
     					indx_edat,    indx_sflg,       indx_uflg )  
				values  ( @jno,         @indx,           'A',              '$',             @hogi,
					     @fstn,        @tstn,           @pltno,           @lstk,           '$',
						 '',           'W',             '0'       ) ;
				set @plstk = @lstk;
			end 
			update tiordx set flag = @flag where ordxkey = @ordxkey;

		end else begin -- 자동창고가 아닌경우
			set @flag = '$Z';
			
			update miplti set plti_rqty = plti_rqty - @oqty
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq = '' ;
			
			delete from miplti
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq = '' 
				and plti_stok = 0
				and plti_rqty = 0;

			update miordi set fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and sdno = @sdno
				and posnr = @posnr;		
			update hiordi set fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and sdno = @sdno
				and posnr = @posnr;	
			if @@ROWCOUNT = 0 begin
				insert into hiordi select * from miordi where docnum = @docnum	and sdno = @sdno and posnr = @posnr;	
			end		

			update tiordx set flag = @flag where ordxkey = @ordxkey;
			insert into hiordx select * from tiordx where ordxkey = @ordxkey;

			-- 이동이력 생성
			insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,       mvht_loc,     mvht_lot,
								mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
								mvht_to_lstk,  mvht_ioflag)
		    			values (@iodate,       @iotime,       @matnr,       @matnrdesc,              @lgort,       @charg, 
								'',            @remark,       @pksz,        @oqty,                   @pltno,       @lstk,
								'Z000000',     '$' )
	

		end
		
		set @lp = @lp + 1;
		
	end
	close c1;
	deallocate c1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_out_exec_date]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_exec_date] -- 실행시는 반드시 lstk로 sort바람
	@credat varchar(8)
AS
begin
	
	declare @ordxkey decimal;
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	declare @pltno varchar(8);
	declare @lstk varchar(7);
	declare @oqty decimal;
	declare @matnr varchar(18)
	declare @lgort varchar(4);
	declare @charg varchar(10);


	declare @plstk varchar(7) = '';
	declare @flag varchar(2) = '';
	declare @jno varchar(18) = '';
	declare @rc int = 0 ;
	declare @indx varchar(4) ;
	declare @hogi varchar(1) ;
	declare @fstn varchar(2) ;
	declare @tstn varchar(2) ;
	declare @lp int = 0 ;

	declare @dts varchar(14) = '' ;
	exec p_curgetdatetime14 @dts output;
	declare @hdate varchar(8) = substring( @dts, 1,8)
	declare @htime varchar(6) = substring( @dts, 9,6)

	declare c1 cursor for
	select b.ordxkey, a.docnum, a.sdno, a.posnr, b.pltno, b.lstk, b.qty, a.matnr, a.lgort, a.charg
	  from miordi a, tiordx b
	where a.docnum = b.docnum
	and a.sdno = b.sdno
	and a.posnr = b.posnr
	and a.credat = @credat
	and b.flag = '$R' order by b.lstk; 

	open c1;

	while(1>0) begin

		fetch c1 into @ordxkey, @docnum, @sdno, @posnr, @pltno, @lstk, @oqty, @matnr, @lgort, @charg;
		if @@FETCH_STATUS <> 0 break;

		if substring(@lstk,1,1) = 'A' begin
			set @flag = '$X';

			if (@lstk <> @plstk) begin			
				update milstk set lstk_io = '$', lstk_stat = '$X'	where lstk_no = @lstk and lstk_stat = '$R'			

				exec @rc = p_get_indx_jno '3',  @jno output
				set @indx = Right(@jno, 4);

				exec p_get_hogi @lstk, @hogi output;
				set @fstn = right('00' + cast(CONVERT(int, @hogi) * 2 as varchar(2)), 2);			
				set @tstn = '43';

				INSERT INTO tbindx  
  	      			 ( indx_jno,     indx_indx,       indx_gubn,        indx_jio,        indx_hogi,   
  		      		   indx_fstn,    indx_tstn,       indx_pltn,        indx_lstk,       indx_xmov,   
     				   indx_edat,    indx_sflg,       indx_uflg )  
			   values  ( @jno,         @indx,           'A',              '$',             @hogi,
						 @fstn,        @tstn,           @pltno,           @lstk,           '$',
						 '',           'W',             '0'       ) ;
				set @plstk = @lstk;
			end 
			update tiordx set flag = @flag where ordxkey = @ordxkey;

		end 
		else begin -- 자동창고가 아닌경우
			set @flag = '$Z';
			
			update miplti set plti_rqty = plti_rqty - @oqty
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq <> 'S' ;
			
			delete from miplti
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq <> 'S' 
				and plti_stok = 0
				and plti_rqty = 0;

			update miordi set  fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and sdno = @sdno
				and posnr = @posnr;		
			update hiordi set fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and sdno = @sdno
				and posnr = @posnr;	
			if @@ROWCOUNT = 0 begin
				insert into hiordi select * from miordi where docnum = @docnum	and sdno = @sdno and posnr = @posnr;	
			end		

			update tiordx set flag = @flag where ordxkey = @ordxkey;
			insert into hiordx select * from tiordx where ordxkey = @ordxkey;
		end
		

		set @lp = @lp + 1;

	end
	close c1;
	DEALLOCATE C1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_out_exec_date_sdno]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_exec_date_sdno] -- 실행시는 반드시 lstk로 sort바람
	@credat varchar(8),
	@orderno varchar(10)
AS
begin
	
	declare @ordxkey decimal;
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	declare @pltno varchar(8);
	declare @lstk varchar(7);
	declare @oqty decimal;
	declare @matnr varchar(18)
	declare @lgort varchar(4);
	declare @charg varchar(10);

	declare @dts varchar(14) = '' ;
	exec p_curgetdatetime14 @dts output;
	declare @hdate varchar(8) = substring( @dts, 1,8)
	declare @htime varchar(6) = substring( @dts, 9,6)

	declare @plstk varchar(7) = '';
	declare @flag varchar(2) = '';
	declare @jno varchar(18) = '';
	declare @rc int = 0 ;
	declare @indx varchar(4) ;
	declare @hogi varchar(1) ;
	declare @fstn varchar(2) ;
	declare @tstn varchar(2) ;
	declare @lp int = 0 ;


	declare c1 cursor for
	select b.ordxkey, a.docnum, a.sdno, a.posnr, b.pltno, b.lstk, b.qty, a.matnr, a.lgort, a.charg
	  from miordi a, tiordx b
	where a.docnum = b.docnum
	and a.sdno = b.sdno
	and a.posnr = b.posnr
	--and a.credat = @credat
	and b.sdno = @orderno
	and b.flag = '$R'; 

	open c1;

	while(1>0) begin

		fetch c1 into @ordxkey, @docnum, @sdno, @posnr, @pltno, @lstk, @oqty, @matnr, @lgort, @charg;
		if @@FETCH_STATUS <> 0 break;

		if substring(@lstk,1,1) = 'A' begin
			set @flag = '$X';
			if (@lstk <> @plstk) begin
				update milstk set lstk_io = '$', lstk_stat = '$X'	where lstk_no = @lstk and lstk_stat = '$R'
				exec @rc = p_get_indx_jno '3',  @jno output
				set @indx = Right(@jno, 4);

				exec p_get_hogi @lstk, @hogi output;
				set @fstn = right('00' + cast(CONVERT(int, @hogi) * 2 as varchar(2)), 2);			
				set @tstn = '43';

				INSERT INTO tbindx  
  	      			 ( indx_jno,     indx_indx,       indx_gubn,        indx_jio,        indx_hogi,   
  		      		   indx_fstn,    indx_tstn,       indx_pltn,        indx_lstk,       indx_xmov,   
     				   indx_edat,    indx_sflg,       indx_uflg )  
			   values  ( @jno,         @indx,           'A',              '$',             @hogi,
						 @fstn,        @tstn,           @pltno,           @lstk,           '$',
						 '',           'W',             '0'       ) ;
				set @plstk = @lstk;
			end 
			update tiordx set flag = @flag where ordxkey = @ordxkey;

		end else begin -- 자동창고가 아닌경우
			set @flag = '$Z';
			
			update miplti set plti_rqty = plti_rqty - @oqty
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq <> 'S' ;
			
			delete from miplti
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq <> 'S' 
				and plti_stok = 0
				and plti_rqty = 0;

			update miordi set fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and sdno = @sdno
				and posnr = @posnr;		
			update hiordi set fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and sdno = @sdno
				and posnr = @posnr;	
			if @@ROWCOUNT = 0 begin
				insert into hiordi select * from miordi where docnum = @docnum	and sdno = @sdno and posnr = @posnr;	
			end		

			update tiordx set flag = @flag where ordxkey = @ordxkey;
			insert into hiordx select * from tiordx where ordxkey = @ordxkey;
		end		

		set @lp = @lp + 1;

	end
	close c1;
	DEALLOCATE C1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_bestq]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_bestq]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@bestq2 varchar(1),
	@cqty decimal,
	@bwlvs varchar(3)
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0

	declare @pltno varchar(8);
	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @sumstok decimal = 0;
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	declare @uqty decimal

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	declare c1 cursor for select plti_pltno, plti_lstk, plti_stok , plti_pksz,  plti_pdesc, plti_remark, plti_idate,  plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0') order by plti_pltno;


	open c1;
	while 1 > 0 begin
		fetch c1 into @pltno, @lstk, @stok, @pksz, @pdesc, @remark, @idate, @itime
		if @@FETCH_STATUS <> 0 break;

		if @cqty > @stok begin					
			set @uqty = @stok;
			set @cqty = @cqty - @stok;
		end else begin
		    set @uqty = @cqty;
			set @cqty = 0
		end

		update miplti set plti_stok = plti_stok - @uqty
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @matnr
			and plti_loc = @lgort
			and plti_lot = @charg
			and plti_bestq = @bestq
			and plti_flag = '1'
			and plti_rqty = 0 ;
		if @@ROWCOUNT = 0 return -1

		update miplti set plti_stok = plti_stok +  @uqty
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @matnr
			and plti_loc = @lgort
			and plti_lot = @charg
			and plti_bestq = @bestq2
		
		if @@ROWCOUNT = 0 begin
			
		  		INSERT INTO miplti  
							( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
							plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
							plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
							plti_pdesc,      plti_oprod,  plti_icust )  
				values (    @pltno,          @lstk,       @matnr,          @lgort,        @charg,
							@bestq2,         @pksz,       @remark,         @uqty,         0,
							@idate,          @idate,      @itime,          '1',           '0',
							@pdesc,          '',          '' );                  
					 
		end			
				
		insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	            	values (@docnum, @tanum, @tapos + 1, @bwlvs, 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음

		insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
		set @sumstok = @sumstok + @uqty		

		if @cqty <= 0 break; 

	end
	close c1;
	DEALLOCATE C1;

	if @sumstok = 0 return -2;

	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @sumstok , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0 return -3;
	
	update hiwmto set fqty = fqty + @sumstok , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = @bwlvs
		   if @@ROWCOUNT = 0 return -100;
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = @bwlvs
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_bestq_spec]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_bestq_spec]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@bestq2 varchar(1),
	@cqty decimal, 
	@pltno varchar(8),
	@bwlvs varchar(3)
	
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0

	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	declare @uqty decimal

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	select @lstk = plti_lstk, @stok = plti_stok , @pksz = plti_pksz,  @pdesc = plti_pdesc, @remark = plti_remark, @idate = plti_idate,  @itime = plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0')
	if @@ROWCOUNT = 0 return-1

	if @cqty > @stok begin					
		set @uqty = @stok;
		set @cqty = @cqty - @stok;
	end else begin
	    set @uqty = @cqty;
		set @cqty = 0
	end

	update miplti set plti_stok = plti_stok - @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 return -1

	update miplti set plti_stok = plti_stok +  @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq2
	
	if @@ROWCOUNT = 0 begin
			
		INSERT INTO miplti  
					( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )  
		values (    @pltno,          @lstk,       @matnr,          @lgort,        @charg,
					@bestq2,         @pksz,       @remark,         @uqty,         0,
					@idate,          @idate,      @itime,          '1',           '0',
					@pdesc,          '',          '' );                  
					 
	end			
				
	insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	           	values (@docnum, @tanum, @tapos + 1, @bwlvs, 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음
			
	insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
	delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0 return -3;
	
	update hiwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = @bwlvs
		   if @@ROWCOUNT = 0 return -100;
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = @bwlvs
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_charg]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_charg]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@charg2 varchar(10),
	@cqty decimal
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0

	declare @pltno varchar(8);
	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @sumstok decimal = 0;
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	declare @uqty decimal

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	declare c1 cursor for select plti_pltno, plti_lstk, plti_stok , plti_pksz,  plti_pdesc, plti_remark, plti_idate,  plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0') order by plti_pltno;


	open c1;
	while 1 > 0 begin
		fetch c1 into @pltno, @lstk, @stok, @pksz, @pdesc, @remark, @idate, @itime;
		if @@FETCH_STATUS <> 0 break;

		if @cqty > @stok begin					
			set @uqty = @stok;
			set @cqty = @cqty - @stok;
		end else begin
		    set @uqty = @cqty;
			set @cqty = 0
		end

		update miplti set plti_stok = plti_stok - @uqty
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @matnr
			and plti_loc = @lgort
			and plti_lot = @charg
			and plti_bestq = @bestq
			and plti_flag = '1'
			and plti_rqty = 0 ;
		if @@ROWCOUNT = 0 return -1

		update miplti set plti_stok = plti_stok +  @uqty
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @matnr
			and plti_loc = @lgort
			and plti_lot = @charg2
			and plti_bestq = @bestq
			and plti_flag = '1'
			and plti_rqty = 0 ;
		if @@ROWCOUNT = 0 begin
			
		  		INSERT INTO miplti  
							( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
							plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
							plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
							plti_pdesc,      plti_oprod,  plti_icust )  
				values (    @pltno,          @lstk,       @matnr,          @lgort,        @charg2,
							@bestq,          @pksz,       @remark,         @uqty,         0,
							@idate,          @idate,      @itime,          '1',           '0',
							@pdesc,          '',          '' );                  
					 
		end			
			
		insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	            	values (@docnum, @tanum, @tapos + 1, '309', 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음

		insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
		set @sumstok = @sumstok + @uqty		

		if @cqty <= 0 break; 

	end
	close c1;
	DEALLOCATE C1;

	if @sumstok = 0  return -2
	
	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @sumstok , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0 return -3;
	
	update hiwmto set fqty = fqty + @sumstok , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = '309'
		   if  @@ROWCOUNT = 0 return -100
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = '309'
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_charg_spec]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_charg_spec]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@charg2 varchar(10),
	@cqty decimal, 
	@pltno varchar(8)
	
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0

	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	declare @uqty decimal

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	select @lstk = plti_lstk, @stok = plti_stok , @pksz = plti_pksz,  @pdesc = plti_pdesc, @remark = plti_remark, @idate = plti_idate,  @itime = plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0')
	if @@ROWCOUNT = 0 return-1

	if @cqty > @stok begin					
		set @uqty = @stok;
		set @cqty = @cqty - @stok;
	end else begin
	    set @uqty = @cqty;
		set @cqty = 0
	end

	update miplti set plti_stok = plti_stok - @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 return -1

	update miplti set plti_stok = plti_stok +  @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg2
		and plti_bestq = @bestq	
	if @@ROWCOUNT = 0 begin
			
		INSERT INTO miplti  
					( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )  
		values (    @pltno,          @lstk,       @matnr,          @lgort,        @charg2,
					@bestq,          @pksz,       @remark,         @uqty,         0,
					@idate,          @idate,      @itime,          '1',           '0',
					@pdesc,          '',          '' );                  
					 
	end			
				
	insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	           	values (@docnum, @tanum, @tapos + 1, '309', 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음
			
	insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
	delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0  return -3;
	
	update hiwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = '309'
		  if  @@ROWCOUNT = 0 return -100
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = '309'
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_lgort]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_lgort]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@lgort2 varchar(4),
	@cqty decimal
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0

	declare @pltno varchar(8);
	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @sumstok decimal = 0;
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	declare @uqty decimal

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	declare c1 cursor for select plti_pltno, plti_lstk, plti_stok , plti_pksz,  plti_pdesc, plti_remark, plti_idate,  plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0') order by plti_pltno;


	open c1;
	while 1 > 0 begin
		fetch c1 into @pltno, @lstk, @stok, @pksz, @pdesc, @remark, @idate, @itime
		if @@FETCH_STATUS <> 0 break;

		if @cqty > @stok begin					
			set @uqty = @stok;
			set @cqty = @cqty - @stok;
		end else begin
		    set @uqty = @cqty;
			set @cqty = 0
		end

		update miplti set plti_stok = plti_stok - @uqty
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @matnr
			and plti_loc = @lgort
			and plti_lot = @charg
			and plti_bestq = @bestq			
		if @@ROWCOUNT = 0 return -1

		update miplti set plti_stok = plti_stok +  @uqty
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @matnr
			and plti_loc = @lgort2
			and plti_lot = @charg
			and plti_bestq = @bestq
			and plti_flag = '1'
			and plti_rqty = 0 ;
		if @@ROWCOUNT = 0 begin
			
		  		INSERT INTO miplti  
							( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
							plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
							plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
							plti_pdesc,      plti_oprod,  plti_icust )  
				values (    @pltno,          @lstk,       @matnr,          @lgort2,       @charg,
							@bestq,          @pksz,       @remark,         @uqty,         0,
							@idate,          @idate,      @itime,          '1',           '0',
							@pdesc,          '',          '' );                  
					 
		end			
				
		insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	            	values (@docnum, @tanum, @tapos + 1, '309', 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음

		insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
		set @sumstok = @sumstok + @uqty		
		
		if @cqty <= 0 break; 

	end
	close c1;
	DEALLOCATE C1;

	if @sumstok = 0 return -2;

	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @sumstok , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0 return -3;
	
	update hiwmto set fqty = fqty + @sumstok , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = '309'
		  if  @@ROWCOUNT = 0 return -100
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = '309'
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltimove_fy]을(를) 만드는 중...';


GO


CREATE  PROCEDURE [dbo].[p_pltimove_fy]
	@lstk varchar(7),	
	@pltno varchar(8), 
	@prod varchar(18),  
	@loc varchar(4), 
	@lot varchar(10), 
	@bestq varchar(1), 
	@stok decimal(13,3), 
	@rqty decimal(13,3),
	@sqty decimal(13,3)  
AS
	
begin
    -- 재고이동임 F->Y or Y->F
	declare @cc integer = 0;
	declare @dlstk varchar(7);
	
	if SUBSTRING(@lstk, 1,1) = 'F' set @dlstk = 'Y000000';
	if SUBSTRING(@lstk, 1,1) = 'Y' set @dlstk = 'F000000';
	if SUBSTRING(@lstk, 2,6) <> '000000' return -1;

	select @cc = count(*) from milstk where lstk_no = @dlstk;
	if @cc = 0 begin
		return -1;
	end; 

	-- 상태 check
	select @cc =count(*) from miplti
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq
	and plti_stok = @stok
	and plti_rqty = @rqty;	
	if @cc = 0 begin
		return -2;
	end;


	-- 일단 야적에 update
	update miplti set plti_stok = plti_stok +  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @dlstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;
	if @@rowcount = 0 begin
		begin try		
			insert into miplti (plti_pltno,  plti_lstk,         plti_prod,  plti_pdesc, plti_oprod, 
			                    plti_loc,    plti_lot,          plti_bestq, plti_pksz,  plti_stok, 
								plti_rqty, 	 plti_cycl_date,    plti_idate, plti_itime, plti_flag, 
								plti_remark, plti_label,        plti_icust )
						 select plti_pltno,  @dlstk,            plti_prod,  plti_pdesc, plti_oprod, 
						        plti_loc,    plti_lot,          plti_bestq, plti_pksz,  @sqty, 
								0,  		 plti_cycl_date,    plti_idate, plti_itime,	'1',
								plti_remark, '0',               plti_icust
			from miplti
			where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @bestq
		end try
		begin catch
			return -999;
		end catch;
	end
	-- 자동창고 수량 감소해 주고	
	update miplti set plti_stok = plti_stok -  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @lstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;
	if @@ERROR <> 0 or @@rowcount = 0 begin
		RETURN -4;
	end;

	-- 해당제품 수량이 zero시 레코드 삭제
	Delete from miplti 
		where plti_pltno = @pltno 
		and plti_lstk = @lstk 
		and plti_prod = @prod 
		and plti_loc = @loc
		and plti_lot = @lot 
		and plti_bestq = @bestq
		and plti_stok = 0 
		and plti_rqty = 0;
	

	RETURN 1;
end;
GO
PRINT N'프로시저 [dbo].[p_pltimove_yardtoyard]을(를) 만드는 중...';


GO


CREATE  PROCEDURE [dbo].[p_pltimove_yardtoyard]
	@lstk varchar(7),	
	@dlstk varchar(7), 
	@pltno varchar(8), 
	@prod varchar(18),  
	@loc varchar(4), 
	@lot varchar(10), 
	@bestq varchar(1),
	@stok decimal(13,3), 
	@rqty decimal(13,3),
	@sqty decimal(13,3)  
AS
	
begin

	declare @cc integer = 0;

	-- 야적위치 존재여부 check
	select @cc = count(*) from milstk where lstk_no = @dlstk;
	if @cc = 0 begin
		return -1;
	end; 

	-- 상태 check
	select @cc =count(*) from miplti
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq
	and plti_stok = @stok
	and plti_rqty = @rqty;	
	if @cc = 0 begin
		return -2;
	end;

	-- 야적에 일단 재고증가
	update miplti set plti_stok = plti_stok +  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @dlstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;
	if @@ERROR <> 0 or @@rowcount = 0 begin
		--없으면 추가
		insert into miplti (plti_pltno,     plti_lstk,  plti_prod,  plti_pdesc, plti_oprod, 
		                    plti_loc,       plti_lot,   plti_bestq, plti_stok,  plti_rqty, 
		                    plti_cycl_date, plti_idate, plti_itime, plti_flag,  plti_remark, 
							plti_label,     plti_icust, plti_pksz)
			         select plti_pltno,     @dlstk,     plti_prod,  plti_pdesc, plti_oprod, 
					        plti_loc,       plti_lot,   plti_bestq, @sqty,      0, 
					        plti_cycl_date, plti_idate, plti_itime, '1',        plti_remark, 
							'0',            plti_icust, plti_pksz
		from miplti
		where plti_pltno = @pltno
		and plti_lstk = @lstk  -- source
		and plti_prod = @prod
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @bestq		

	end;		

	
	-- source는 감소해 주고	
	update miplti set plti_stok = plti_stok -  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @lstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;
	if @@ERROR <> 0 or @@rowcount = 0 begin
		RETURN -4;
	end;

	-- 해당제품 수량이 zero시 레코드 삭제
	Delete from miplti 
		where plti_pltno = @pltno 
		and plti_lstk = @lstk 
		and plti_prod = @prod 
		and plti_loc = @loc
		and plti_lot = @lot 
		and plti_bestq = @bestq
		and plti_stok = 0 
		and plti_rqty = 0;
	

	RETURN 1;
end;
GO
PRINT N'프로시저 [dbo].[p_pltimove_yloc]을(를) 만드는 중...';


GO


CREATE  PROCEDURE [dbo].[p_pltimove_yloc]
	@lstk varchar(7),	
	@dlstk varchar(7), 
	@pltno varchar(8), 
	@prod varchar(18),  
	@loc varchar(4), 
	@lot varchar(10), 
	@bestq varchar(1), 
	@stok decimal(13,3), 
	@rqty decimal(13,3),
	@sqty decimal(13,3)  
AS
	
begin

	declare @cc integer = 0;

	-- Y010101 ok
	if substring(@dlstk,1,1) <> 'Y' return -1;
	if substring(@dlstk,2,6) = '000000' return -2;

	-- 야적위치 존재여부 check
	select @cc = count(*) from milstk where lstk_no = @dlstk;
	if @cc = 0 begin
		return -3;
	end; 
	
	
	-- 상태 check
	select @cc =count(*) from miplti
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq
	and plti_stok = @stok
	and plti_rqty = @rqty;	
	if @cc = 0 begin
		return -4;
	end;

-- 일단 야적에 update
	update miplti set plti_stok = plti_stok +  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @dlstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;	
	if @@rowcount = 0 begin
		begin try
			insert into miplti (plti_pltno,  plti_lstk,      plti_prod,  plti_pdesc, plti_oprod, 
			                    plti_loc,    plti_lot,       plti_bestq, plti_pksz,  plti_stok, 
								plti_rqty,   plti_cycl_date, plti_idate, plti_itime, plti_flag, 
								plti_remark, plti_label,     plti_icust )
						 select plti_pltno,  @dlstk,         plti_prod,  plti_pdesc, plti_oprod, 
						        plti_loc,    plti_lot,       plti_bestq, plti_pksz,  @sqty, 
								0,           plti_cycl_date, plti_idate, plti_itime, '1', 
								plti_remark, '0',            plti_icust
			from miplti
			where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @bestq;
			if @@rowcount = 0 or @@ERROR <> 0 return -99;	
		end try
		begin catch
			return -999;
		end catch	
	End;
	-- 자동창고 수량 감소해 주고	
	update miplti set plti_stok = plti_stok -  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @lstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;	
	if @@ERROR <> 0 or @@rowcount = 0 begin
		RETURN -5;
	end;

	-- 해당제품 수량이 zero시 레코드 삭제
	Delete from miplti 
		where plti_pltno = @pltno 
		and plti_lstk = @lstk 
		and plti_prod = @prod 
		and plti_loc = @loc
		and plti_lot = @lot 
		and plti_bestq = @bestq
		and plti_stok = 0 
		and plti_rqty = 0;
	

	RETURN 1;
end;
GO
PRINT N'프로시저 [dbo].[p_pltimoveto_yard]을(를) 만드는 중...';


GO


CREATE  PROCEDURE [dbo].[p_pltimoveto_yard]
	@lstk varchar(7),	
	@dlstk varchar(7), 
	@pltno varchar(8), 
	@prod varchar(18),  
	@loc varchar(4), 
	@lot varchar(10), 
	@bestq varchar(1), 
	@stok decimal(13,3), 
	@rqty decimal(13,3),
	@sqty decimal(13,3)  
AS
	
begin

	declare @cc integer = 0;

	-- 야적위치 존재여부 check
	select @cc = count(*) from milstk where lstk_no = @dlstk;
	if @cc = 0 begin
		return -1;
	end; 

	-- 상태 check
	select @cc =count(*) from miplti
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq
	and plti_stok = @stok
	and plti_rqty = @rqty;	
	if @cc = 0 begin
		return -2;
	end;

	-- 야적에 일단 재고증가
	update miplti set plti_stok = plti_stok +  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @dlstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;
	if @@ERROR <> 0 or @@rowcount = 0 begin
		--없으면 추가
		insert into miplti (plti_pltno,     plti_lstk,  plti_prod,  plti_pdesc, plti_oprod, 
		                    plti_loc,       plti_lot,   plti_bestq, plti_stok,  plti_rqty, 
		                    plti_cycl_date, plti_idate, plti_itime, plti_flag,  plti_remark, 
							plti_label,     plti_icust, plti_pksz)
			         select plti_pltno,     @dlstk,     plti_prod,  plti_pdesc, plti_oprod, 
					        plti_loc,       plti_lot,   plti_bestq, @sqty,      0, 
					        plti_cycl_date, plti_idate, plti_itime, '1',        plti_remark, 
							'0',            plti_icust, plti_pksz
		from miplti
		where plti_pltno = @pltno
		and plti_lstk = @lstk  -- source
		and plti_prod = @prod
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @bestq		

	end;		

	-- 자동창고 수량 감소해 주고	
	update miplti set plti_stok = plti_stok -  @sqty 
	where plti_pltno = @pltno 
	and plti_lstk = @lstk 
	and plti_prod = @prod 
	and plti_loc = @loc
	and plti_lot = @lot 
	and plti_bestq = @bestq;
	if @@ERROR <> 0 or @@rowcount = 0 begin
		RETURN -4;
	end;

	-- 해당제품 수량이 zero시 레코드 삭제
	Delete from miplti 
		where plti_pltno = @pltno 
		and plti_lstk = @lstk 
		and plti_prod = @prod 
		and plti_loc = @loc
		and plti_lot = @lot 
		and plti_bestq = @bestq
		and plti_stok = 0 
		and plti_rqty = 0;
	
	-- 자동창고면 location master 빈셀로 만든다
	
	
	if SUBSTRING(@lstk ,1,1) = 'A' begin

		select @cc = count(*) from miplti where plti_lstk = @lstk;
		if @cc = 0 begin
			update milstk set  lstk_io = '0', lstk_stat = '00' where lstk_no = @lstk;
		end;
	end;

	RETURN 0;
end;
GO
PRINT N'프로시저 [dbo].[p_pltzadd]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltzadd]
	@pltno varchar(8),
	@npltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@stok decimal,
	@sqty decimal,
	@labelyn int
AS
begin
	declare @pksz decimal(18,3);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @pdesc varchar(40);
	declare @canqty int;
	declare @mlabel varchar(1);
	declare @remark varchar(40);
	declare @oprod varchar(18);

	declare @dts varchar(19);
	declare @cnt int = 0;
	declare @pltcnt int = 0;
	declare @sumqty decimal = 0;
	declare	@prnno varchar(1); 

	select @canqty = mast_canqty from mimast where mast_cd = @prod;
	if @@ROWCOUNT = 0 return -1;

	-- step 상태첵크
	select @cnt = count(*) from miplti where plti_pltno = @npltno and plti_lstk = @lstk;
	if @cnt = 0 return -2;
	
	select @pdesc = plti_pdesc, @pksz = plti_pksz, @idate = plti_idate, @remark = plti_remark, @oprod = plti_oprod from miplti
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq
	and   plti_stok = @stok
	and   plti_stok >= @sqty
	and   plti_rqty = 0;
	if @@ROWCOUNT = 0 return -3;

	update miplti set plti_stok = plti_stok - @sqty
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq ;
	if @@ROWCOUNT = 0 return -4;

	
	select @dts = convert(varchar(19), getdate(), 121) from tbstat;
	set @cdate = substring(@dts, 1,4) + '/' + substring(@dts, 6,2) + '/' + substring(@dts, 9,2);
	set @idate = @cdate;
	set @itime =  substring(@dts, 12,2) + ':' + substring(@dts, 15,2) + ':' + substring(@dts, 18,2);

	if @labelyn = 1 set @mlabel  = '1';
	else set @mlabel  = '0';
		
	begin try
		
		update miplti set plti_stok = plti_stok + @sqty
		where plti_pltno = @npltno
		and plti_lstk = @lstk
		and plti_prod = @prod
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @bestq ;
		if @@ROWCOUNT = 0 begin
			INSERT INTO miplti  
				  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )
		    select  @npltno,         plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     @sqty,          0,   
					@cdate,          @idate,      @itime,          '1',           @mlabel,
					plti_pdesc,      plti_oprod,  plti_icust  
			from miplti
			where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @bestq;	
			if @@ROWCOUNT = 0 return -99;

		end
		
		update miplti set plti_label = '1' where plti_pltno = @npltno and plti_lstk = @lstk ; --혼적 할수 있으므로

		select @pltcnt = count(*), @sumqty = sum(plti_stok) from miplti where plti_pltno = @npltno;
		
		if @labelyn = 1 begin
	
			if SUBSTRING(@lstk,1,1) = 'F' set @prnno = '2';
			else set @prnno = '1';

			if @pltcnt = 1 begin
				INSERT INTO tbbprn  
  		  			  		(prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
				values     ( @prnno,   @npltno,        @prod,     @pdesc,     @lot,      @pksz,      @sumqty,     1,            GETDATE() );
			end
			else begin
				INSERT INTO tbbprn  
  		  			  		(prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
				values     ( @prnno,   @npltno,        '',       '',         '',        0.00,       @sumqty,     @pltcnt,      GETDATE() );
			end
		end
		
	end try
	begin catch 
		return -999;
	end catch;

	delete from miplti 	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq 
	and plti_stok = 0
	and plti_rqty = 0; 

	RETURN 1;
END
GO
PRINT N'프로시저 [dbo].[p_pltzerall]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltzerall]
	@pltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@stok decimal,
	@sqty decimal,
	@labelyn int
AS
begin
	declare @pksz decimal(18,3);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @pdesc varchar(40);
	declare @canqty int;
	declare @mlabel varchar(1);
	declare @remark varchar(40);
	declare @oprod varchar(18);

	declare @dts varchar(19);
	declare @npltno varchar(8);
	declare @prnno varchar(1);  -- F 공장 프린터 번호 2  Y:메인 프린터 번호 1

	select @canqty = mast_canqty from mimast where mast_cd = @prod;
	if @@ROWCOUNT = 0 return -1; -- 제품코드등록바람

	if @sqty <= 0 return -2;     -- 선택수량 없음
	if @stok < @sqty return -3;  -- 선택수량 너무큼

	-- step 상태첵크
	
	select @pdesc = plti_pdesc, @pksz = plti_pksz, @idate = plti_idate, @remark = plti_remark, @oprod = plti_oprod from miplti
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq
	and   plti_stok = @stok
	and   plti_rqty = 0;
	if @@ROWCOUNT = 0 return -4;	 -- 상태변함

	while (@sqty > 0) begin
		if @sqty > @canqty set  @sqty = @sqty - @canqty;
		else begin
			set @canqty = @sqty;
			set @sqty = 0;
		end

		update miplti set plti_stok = plti_stok - @canqty
		where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @prod
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @bestq ;
		if @@ROWCOUNT = 0 return -5;  -- 상태변함2

		declare @rc int = 0;
		exec @rc = p_getpltno @npltno output;
		if @rc = 0 return -6          -- 파렛번호 얻기 실패
		if len(@npltno) <> 8 return -7;  -- 파렛번호 얻기 실패2

		select @dts = convert(varchar(19), getdate(), 121) from tbstat;
		set @cdate = substring(@dts, 1,4) + '/' + substring(@dts, 6,2) + '/' + substring(@dts, 9,2);
		set @idate = @cdate;
		set @itime =  substring(@dts, 12,2) + ':' + substring(@dts, 15,2) + ':' + substring(@dts, 18,2);

		if @labelyn = 1 set @mlabel  = '1';
		else set @mlabel  = '0';
		
		begin try
		
			INSERT INTO miplti  
					 ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					   plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					   plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					   plti_pdesc,      plti_oprod,  plti_icust )  
			values (   @npltno,         @lstk,       @prod,           @loc,          @lot,
					   @bestq,          @pksz,       @remark,         @canqty,       0,
					   @cdate,          @idate,      @itime,          '1',           @mlabel,
					   @pdesc,          @oprod,      '' );                  
		
		
			if @labelyn = 1 begin
	
				if SUBSTRING(@lstk,1,1) = 'F' set @prnno = '2';
				else set @prnno = '1';

				INSERT INTO tbbprn  
  		  		  		   (prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
				values    ( @prnno,   @npltno,       @prod,     @pdesc,     @lot,      @pksz,      @canqty,   1,            GETDATE() );
			end
		
		end try
		begin catch 
			return -999;   -- 파렛번호 이미 발행
		end catch;
	end --end while

	delete from miplti 	
	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq 
	and plti_stok = 0
	and plti_rqty = 0; 

	RETURN 1;
END
GO
PRINT N'프로시저 [dbo].[p_pltznew]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltznew] 
	@pltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@stok decimal,
	@sqty decimal,
	@labelyn int
AS
begin
	declare @pksz decimal(18,3);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @pdesc varchar(40);
	declare @canqty int;
	declare @mlabel varchar(1);
	declare @remark varchar(40);
	declare @oprod varchar(18);

	declare @dts varchar(19);
	declare @npltno varchar(8);
	declare @prnno varchar(1);  -- F 공장 프린터 번호 2  Y:메인 프린터 번호 1

	select @canqty = mast_canqty from mimast where mast_cd = @prod;
	if @@ROWCOUNT = 0 return -1;

	-- step 상태첵크	
	select @pdesc = plti_pdesc, @pksz = plti_pksz, @idate = plti_idate, @remark = plti_remark, @oprod = plti_oprod from miplti
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq
	and   plti_stok = @stok
	and   plti_stok >= @sqty
	and   plti_rqty = 0;
	if @@ROWCOUNT = 0 return -2;

	update miplti set plti_stok = plti_stok - @sqty
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq ;
	if @@ROWCOUNT = 0 return -3;

	declare @rc int  = 0;
	exec @rc = p_getpltno @npltno output;
	if @rc <> 1 return -4
	if len(@npltno) <> 8 return -5;

	select @dts = convert(varchar(19), getdate(), 121) from tbstat;
	set @cdate = substring(@dts, 1,4) + '/' + substring(@dts, 6,2) + '/' + substring(@dts, 9,2);
	set @idate = @cdate;
	set @itime =  substring(@dts, 12,2) + ':' + substring(@dts, 15,2) + ':' + substring(@dts, 18,2);

	if @labelyn = 1 set @mlabel  = '1'
	else set @mlabel  = '0';
		
	begin try
		
		INSERT INTO miplti  
				  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )
		    select  @npltno,         plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     @sqty,          0,   
					@cdate,          @idate,      @itime,          '1',           @mlabel,
					plti_pdesc,      plti_oprod,  plti_icust  
			from miplti
			where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @bestq;	
			if @@ROWCOUNT = 0 return -99;
		
		if @labelyn = 1 begin
	
			if SUBSTRING(@lstk,1,1) = 'F' set @prnno = '2'
			else set @prnno = '1';

			INSERT INTO tbbprn  
  		  		  		(prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
			values     ( @prnno,   @npltno,        @prod,     @pdesc,     @lot,      @pksz,      @sqty,     1,            GETDATE() )
		end
		
	end try
	begin catch 
		return -999
	end catch;

	delete from miplti 	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq 
	and plti_stok = 0
	and plti_rqty = 0; 

	RETURN 1;
END
GO
PRINT N'프로시저 [dbo].[p_reassign_cell]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_reassign_cell]
	@jno varchar(18),
	@gubn varchar(1),
	@jio varchar(1),
	@pltno varchar(8),
	@lstk varchar(7),
	@sflg varchar(1),
	@prod varchar(18),
	@nlstk varchar (7),
	@hogi varchar (1),
	@tstn varchar (2)
AS
begin
	
	update tbindx 
		set indx_hogi = @hogi,
			indx_pltn = @pltno,
			indx_lstk = @nlstk,
			indx_tstn = @tstn
	where indx_jno  = @jno
	  and indx_sflg = 'P'; 
	if @@ROWCOUNT = 0 return -1; -- 데이타 상태가 변했읍니다(tbindx)
	
	update milstk set lstk_io = 'I', lstk_stat = 'IX' where lstk_no = @nlstk  and lstk_io = '0';
	if @@ROWCOUNT = 0 return -2; -- 목적셀의 상태가 변했읍니다(To location)
	
	update milstk set lstk_io = '0', lstk_stat = '00' where lstk_no = @lstk  and lstk_io = 'I';
	if @@ROWCOUNT = 0 return -3; -- 시작셀 상태가 변했읍니다(from location)
	
	update miplti set plti_lstk = @nlstk where plti_pltno = @pltno  and plti_lstk  = @lstk;
	if @@ROWCOUNT = 0 return -4; -- 재고 상태가 변했읍니다(miplti)

	RETURN 1
end
GO
PRINT N'프로시저 [dbo].[p_rsrv_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_rsrv_cancel]
	@docnum varchar(16),
	@sdno varchar(10),
	@posnr int,
	@ordxkey decimal,
	@pltno varchar(8),
	@lstk varchar(7),
	@oqty decimal

AS
begin

declare @lc int = 0;

	--exec p_tilock;
	declare @matnr varchar(18)
	declare @lgort varchar(4)
	declare @charg varchar(10)
	declare @bestq varchar(1)

	select @matnr = matnr, @lgort = lgort, @charg = charg from miordi where docnum = @docnum and sdno = @sdno and posnr = @posnr;
	if @@ROWCOUNT = 0 return -1;  -- 상태변함 miordi

	delete from tiordx where ordxkey = @ordxkey and flag = '$R';
	if @@ROWCOUNT = 0 return -2;  -- 상태변함 tiordx

	update miplti set plti_stok = plti_stok + @oqty, 
	                  plti_rqty = plti_rqty - @oqty
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
     and plti_prod = @matnr
	  and plti_loc = @lgort
	  and plti_lot = @charg
	  and plti_bestq = ''
	if @@ROWCOUNT = 0 return -3;  -- 상태변함 miplti

	if SUBSTRING(@lstk, 1,1) = 'A' begin
		select @lc =count(*) from tiordx where lstk = @lstk;
		if @lc = 0 begin
			update milstk set lstk_io = '0', lstk_stat = '10' 
			where lstk_no = @lstk
			  and 0 = (select count(*) from miplti 
				        where plti_lstk = @lstk
            		    and plti_rqty > 0 ) ;

		end
	end

	update miordi set rqty = rqty - @oqty
	where  docnum = @docnum
	and    sdno  = @sdno
	and    posnr  = @posnr;
	if @@ROWCOUNT = 0 return -4;  -- 상태변함 miordi

RETURN 1
end
GO
PRINT N'프로시저 [dbo].[p_rsrv_upper_line]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_rsrv_upper_line]
	@docnum varchar(16), 
	@sdno varchar(10), 
	@posnr int,
	@matnr varchar(18), 
	@lgort varchar(4), 
	@charg varchar(10)
AS
begin
	declare @wecust varchar(17);
	declare @oq decimal;
	declare @rq decimal;
	declare @sq decimal;
	declare @oqty decimal;

	declare @date varchar(8);
	declare @time varchar(6);

	declare @canqty decimal = 1;
	
	
	declare @ho1 varchar(1) = '1';
	declare @ho2 varchar(1) = '2';
	declare @ho3 varchar(1) = '3';
	declare @ho4 varchar(1) = '4';
	declare @ho5 varchar(1) = '5';
	declare @scrc_gbun varchar(1);
	declare @scrc_onln varchar(1);
	declare @scrc_emer varchar(1);
	declare @scrc_ouse varchar(1);
	declare @scrc_comm varchar(1);

	declare @dumy int;
	declare @pltno varchar(8);
	declare @loca varchar(7);
	declare @pstok decimal;
	declare @prq decimal;
	declare @pksz decimal(18,3);
	declare @remark varchar(40);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);

	declare @oprod varchar(18);

	
	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '01';
	if @scrc_ouse = '0' set @ho1 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '02';
	if @scrc_ouse = '0' set @ho2 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '03';
	if @scrc_ouse = '0' set @ho3 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '04';
	if @scrc_ouse = '0' set @ho4 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '05';
	if @scrc_ouse = '0' set @ho5 = '9';
		 
	declare @rc int = 0;
	declare @lp int = 0;
	declare @dts varchar(14) = '';

	declare @odate varchar(8);
	declare @otime varchar(6);
	
	exec @rc = p_curgetdatetime14 @dts output;	
	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);

	-- lock ----
	--exec p_tilock;
	
	declare c1 cursor for
    SELECT wecust, matnr, charg, lgort, qty, rqty
      FROM miordi   
	where docnum = @docnum
	  and sdno = @sdno
	  and posnr = @posnr
	  and matnr = @matnr
	  and lgort = @lgort
	  and charg = @charg
	  and qty - rqty > 0 ;

	open c1;
	while 1 > 0 begin
		fetch c1 into @wecust,@matnr,@charg,@lgort,@oq,@rq;
		if @@FETCH_STATUS <> 0 break;
		
		select @canqty = mast_canqty from mimast where mast_cd = @matnr;
	
		set @sq = @oq - @rq;		
		while @sq > 0 begin

			if @canqty <= 0 set @canqty = 1

			if @sq >= @canqty begin
				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    plti_stok >= @canqty
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) ORDER BY 1, 4 desc, 8, 9 ;
				if @@ROWCOUNT = 0 begin 
					Select top 1 
						@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
						@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
					from v_rsrv 
					where  plti_prod = @matnr
					and    plti_loc = @lgort
					and    plti_lot = @charg
					and    plti_bestq = ''
					and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) ORDER BY 1, 4 desc, 8, 9 ;
				end		
			end
			else begin
				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) ORDER BY 1, 4 desc, 8, 9 ;
			end

			if substring(@loca, 1, 1) = 'A' begin
				update milstk set lstk_io = '$', lstk_stat = '$R'  where lstk_no = @loca ;
			end

			if @sq > @pstok  begin --large order so fetch again
				update miplti set plti_stok = plti_stok - @pstok, plti_rqty = plti_rqty + @pstok
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
			
				set @oqty = @pstok;
				set @sq = @sq - @pstok;
			end
			else begin     -- large plti to scr again
				update miplti set plti_stok = plti_stok - @sq, plti_rqty = plti_rqty + @sq
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
		
				set @oqty = @sq;
				set @sq = 0; 
			end

			INSERT INTO tiordx  
		 			 ( docnum,  sdno,  posnr,  lstk,   pltno,   qty,    flag,  pksz, credat,  cretim,   remark, idate,  itime,  oprod )  
		      VALUES ( @docnum, @sdno, @posnr, @loca,  @pltno,  @oqty, '$R',  @pksz, @odate,  @otime,  @remark, @idate, @itime, @oprod) ;

  
			update miordi set rqty = rqty + @oqty
			where  docnum = @docnum
			and    sdno = @sdno
			and    posnr = @posnr
			and    qty - rqty > 0 ;
				
			set @lp = @lp + 1;
		end
		
	end
	close c1;
	deallocate c1;

	RETURN @lp;
end
GO
PRINT N'프로시저 [dbo].[p_rsrv_upper_line2]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_rsrv_upper_line2]
	@docnum varchar(16), 
	@sdno varchar(10), 
	@posnr int,
	@matnr varchar(18), 
	@lgort varchar(4), 
	@charg varchar(10)
AS
begin
	declare @wecust varchar(17);
	declare @oq decimal;
	declare @rq decimal;
	declare @sq decimal;
	declare @oqty decimal;

	declare @date varchar(8);
	declare @time varchar(6);

	declare @canqty decimal = 1;
	
	
	declare @ho1 varchar(1) = '1';
	declare @ho2 varchar(1) = '2';
	declare @ho3 varchar(1) = '3';
	declare @ho4 varchar(1) = '4';
	declare @ho5 varchar(1) = '5';
	declare @scrc_gbun varchar(1);
	declare @scrc_onln varchar(1);
	declare @scrc_emer varchar(1);
	declare @scrc_ouse varchar(1);
	declare @scrc_comm varchar(1);

	declare @dumy int;
	declare @pltno varchar(8);
	declare @loca varchar(7);
	declare @pstok decimal;
	declare @prq decimal;
	declare @pksz decimal(18,3);
	declare @remark varchar(40);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);

	declare @oprod varchar(18);

	
	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '01';
	if @scrc_ouse = '0' set @ho1 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '02';
	if @scrc_ouse = '0' set @ho2 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '03';
	if @scrc_ouse = '0' set @ho3 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '04';
	if @scrc_ouse = '0' set @ho4 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '05';
	if @scrc_ouse = '0' set @ho5 = '9';
		 
	declare @rc int = 0;
	declare @lp int = 0;
	declare @dts varchar(14) = '';

	declare @odate varchar(8);
	declare @otime varchar(6);
	
	exec @rc = p_curgetdatetime14 @dts output;	
	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);

	-- lock ----
	--exec p_tilock;
	
	declare c1 cursor for
    SELECT wecust, matnr, charg, lgort, qty, rqty
      FROM miordi   
	where docnum = @docnum
	  and sdno = @sdno
	  and posnr = @posnr
	  and matnr = @matnr
	  and lgort = @lgort
	  and charg = @charg
	  and qty - rqty > 0 ;

	open c1;
	while 1 > 0 begin
		fetch c1 into @wecust,@matnr,@charg,@lgort,@oq,@rq;
		if @@FETCH_STATUS <> 0 break;
		
		select @canqty = mast_canqty from mimast where mast_cd = @matnr;
	
		set @sq = @oq - @rq;		
		while @sq > 0 begin

			--if canqty <= 0 set @canqty = 1

			if @sq >= @canqty begin
				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    plti_stok >= @canqty
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) ORDER BY 1, 4 desc, 8, 9 ;
				if @@ROWCOUNT = 0 begin 
					Select top 1 
						@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
						@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
					from v_rsrv 
					where  plti_prod = @matnr
					and    plti_loc = @lgort
					and    plti_lot = @charg
					and    plti_bestq = ''
					and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) ORDER BY 1, 4 desc, 8, 9 ;
				end		
			end
			else begin
				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) ORDER BY 1, 4 desc, 8, 9 ;
			end

			if substring(@loca, 1, 1) = 'A' begin
				update milstk set lstk_io = '$', lstk_stat = '$R'  where lstk_no = @loca ;
			end

			if @sq > @pstok  begin --large order so fetch again
				update miplti set plti_stok = plti_stok - @pstok, plti_rqty = plti_rqty + @pstok
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
			
				set @oqty = @pstok;
				set @sq = @sq - @pstok;
			end
			else begin     -- large plti to scr again
				update miplti set plti_stok = plti_stok - @sq, plti_rqty = plti_rqty + @sq
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
		
				set @oqty = @sq;
				set @sq = 0; 
			end

			INSERT INTO tiordx  
		 			 ( docnum,  sdno,  posnr,  lstk,   pltno,   qty,    flag,  pksz, credat,  cretim,   remark, idate,  itime,  oprod )  
		      VALUES ( @docnum, @sdno, @posnr, @loca,  @pltno,  @oqty, '$R',  @pksz, @odate,  @otime,  @remark, @idate, @itime, @oprod) ;

  
			update miordi set rqty = rqty + @oqty
			where  docnum = @docnum
			and    sdno = @sdno
			and    posnr = @posnr
			and    qty - rqty > 0 ;
				
			set @lp = @lp + 1;
		end
		
	end
	close c1;
	deallocate c1;

	RETURN @lp;
end
GO
PRINT N'프로시저 [dbo].[p_tilock]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_tilock]
AS
begin
	declare @ll decimal  = 0;

	select @ll = lock_cnt from tilock;

	if @ll < 30000 set @ll = 0;
	else set @ll = @ll  + 1;

	update tilock set lock_cnt = @ll;

	RETURN 1
end
GO
PRINT N'프로시저 [dbo].[s_getbachasno]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[s_getbachasno]
	@bdate varchar(10)
AS
begin
	declare @sno int = 0;
	declare @bachadate varchar(10) = '';

	select @sno = sno from mibacha where bachadate = @bdate;
	if @@ROWCOUNT = 0 begin
		insert into mibacha ( bachadate, sno ) values ( @bdate, 1);
		return 1;
	end
	update mibacha set sno = @sno + 1 where bachadate = @bdate and sno = @sno;
	RETURN @sno + 1;
end
GO
PRINT N'프로시저 [dbo].[s_loadcncl_all]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[s_loadcncl_all]
	@bachadate varchar(10),
	@car_no varchar(20),
	@seq int,
	@load_qty decimal
AS
begin
	declare @cc int = 0;
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	declare @ordi_size decimal(18,3);
	declare @ordi_seq int;
	
	declare @lp int = 0;
	declare @ordi_ltqty decimal(18,3) = 0;
	declare @ordi_totqty decimal = 0;
	declare @ordi_oqty decimal = 0;


	select @cc = count(*) from tacar where bachadate = @bachadate and car_no = @car_no and seq = @seq and load_qty = @load_qty;
	if @@ROWCOUNT = 0 return -1;
	
	declare c1 cursor for
	select docnum, sdno, posnr, ordi_size, qty, ordi_seq from  taordi
	where bachadate = @bachadate and car_no = @car_no and car_sno = @seq for update;
	 
	 open c1;
	 while(1>0) begin
		fetch c1 into @docnum, @sdno, @posnr, @ordi_size, @ordi_oqty, @ordi_seq;
		if @@FETCH_STATUS <> 0 break;

		update taordi set bachadate = '', car_no = '', car_step = '0', car_sno = 0, print_step = '0' where current of c1;
		if @@ROWCOUNT = 0 break;
		set @lp = @lp + 1; 

		set @ordi_totqty = @ordi_totqty + @ordi_oqty;
	    set @ordi_ltqty = @ordi_ltqty + @ordi_size * @ordi_oqty

	 end
	 close c1;
	 deallocate c1;

	 if @lp = 0 return -1;

	 update tacar set load_qty = load_qty - @ordi_totqty,
	                  load_vol = load_vol - @ordi_ltqty
     where bachadate = @bachadate and car_no = @car_no and seq = @seq;
	 if @@ROWCOUNT = 0 return -2;

	 update tacar set load_qty = 0, load_vol = 0, bachadate = '',  seq = 0,  remark = '', step = '0'
	 where car_no = @car_no and ( load_qty <= 0 or load_vol <= 0 );
	 

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[s_loadcncl_all_etc]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[s_loadcncl_all_etc]
	@bachadate varchar(10),
	@car_no varchar(20),
	@seq int,
	@load_qty decimal
AS
begin
	declare @cc int = 0;
	declare @docnum varchar(16);
	declare @tanum decimal;
	declare @tapos int;
	declare @pksz decimal(18,3);
	declare @ordi_seq int;
	
	declare @lp int = 0;
	declare @ordi_ltqty decimal(18,3) = 0;
	declare @ordi_totqty decimal = 0;
	declare @ordi_oqty decimal = 0;


	select @cc = count(*) from tacar where bachadate = @bachadate and car_no = @car_no and seq = @seq and load_qty = @load_qty and flag = '1';
	if @@ROWCOUNT = 0 return -1;
	
	declare c1 cursor for
	select docnum, tanum, tapos, pksz, vsolm, ordi_seq from  tawmto
	where bachadate = @bachadate and car_no = @car_no and car_sno = @seq for update;
	 
	 open c1;
	 while(1>0) begin
		fetch c1 into @docnum, @tanum, @tapos, @pksz, @ordi_oqty, @ordi_seq;
		if @@FETCH_STATUS <> 0 break;

		update tawmto set bachadate = '', car_no = '', car_step = '0', car_sno = 0, print_step = '0' where current of c1;
		if @@ROWCOUNT = 0 break;
		set @lp = @lp + 1; 

		set @ordi_totqty = @ordi_totqty + @ordi_oqty;
	    set @ordi_ltqty = @ordi_ltqty + @pksz * @ordi_oqty

	 end
	 close c1;
	 deallocate c1;

	 if @lp = 0 return -1;

	 update tacar set load_qty = load_qty - @ordi_totqty,
	                  load_vol = load_vol - @ordi_ltqty
     where bachadate = @bachadate and car_no = @car_no and seq = @seq;
	 if @@ROWCOUNT = 0 return -2;

	 update tacar set load_qty = 0, load_vol = 0, bachadate = '',  seq = 0,  remark = '', step = '0', flag = ''
	 where car_no = @car_no and ( load_qty <= 0 or load_vol <= 0 );
	 

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[u_inpt_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_inpt_cancel]
	@gb int,
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);


	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare @lp int = 0;

	if @gb = 1  set @dloca = 'Y000000';
	if @gb = 2  set @dloca = 'F000000';

	exec p_tilock;

	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;

	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,   @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
		if @@FETCH_STATUS <> 0 break;

		update miplti set plti_stok = plti_stok + @plti_stok  -- 야적에 쓰고
		where plti_pltno = @pltno
		and plti_lstk = @dloca
		and plti_prod = @plti_prod
		and plti_loc = @plti_loc
		and plti_lot = @plti_lot
		and plti_bestq = @plti_bestq;
		if @@ROWCOUNT = 0 begin  -- 없으면 insert
			insert into miplti (plti_pltno,    plti_lstk,        plti_prod,    plti_pdesc,    plti_oprod,    plti_loc,   
								plti_lot,      plti_bestq,       plti_pksz,    plti_remark,   plti_icust,    plti_stok,   
								plti_rqty,     plti_cycl_date,   plti_idate,   plti_itime,    plti_flag,     plti_label)
		              values (  @pltno,        @dloca,           @plti_prod,   @plti_pdesc,   @plti_oprod,   @plti_loc,  
		                        @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,  @plti_icust,   @plti_stok,
					            @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,   @plti_flag,    @plti_label );

		end
		-- 글구 지운다
		delete from miplti where current of c1;
		--where plti_pltno = @pltno
		--and plti_lstk = @dloca
		--and plti_prod = @plti_prod
		--and plti_loc = @plti_loc
		--and plti_lot = @plti_lot
		--and plti_bestq = @plti_bestq;		

		set @lp = @lp + 1
	end
	close c1;

	if @lp > 0 begin
		update milstk set lstk_io = '0', lstk_stat = '00' where lstk_no = @alstk;
	end

	deallocate c1;
	RETURN @lp
end
GO
PRINT N'프로시저 [dbo].[u_inpt_double]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_inpt_double]
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);


	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare  @lp int = 0

    set @dloca = 'Y000000';
	
	exec p_tilock;

	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;

	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,   @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
		if @@FETCH_STATUS <> 0 break;

		update miplti set plti_stok = plti_stok + @plti_stok  -- 야적에 쓰고
		where plti_pltno = @pltno
		and plti_lstk = 'Y000000'
		and plti_prod = @plti_prod
		and plti_loc = @plti_loc
		and plti_lot = @plti_lot
		and plti_bestq = @plti_bestq;
		if @@ROWCOUNT = 0 begin  -- 없으면 insert
			insert into miplti (plti_pltno,    plti_lstk,        plti_prod,    plti_pdesc,    plti_oprod,    plti_loc,   
								plti_lot,      plti_bestq,       plti_pksz,    plti_remark,   plti_icust,    plti_stok,   
								plti_rqty,     plti_cycl_date,   plti_idate,   plti_itime,    plti_flag,     plti_label)
		              values (  @plti_pltno,   'Y000000',        @plti_prod,   @plti_pdesc,   @plti_oprod,   @plti_loc,  
		                        @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,  @plti_icust,   @plti_stok,
					            @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,   @plti_flag,    @plti_label );

		end
		-- 글구 지운다
		delete from miplti where current of c1;
		--where plti_pltno = @pltno
		--and plti_lstk = @dloca
		--and plti_prod = @plti_prod
		--and plti_loc = @plti_loc
		--and plti_lot = @plti_lot
		--and plti_bestq = @plti_bestq;
		set @lp = @lp + 1
	end
	close c1;
	deallocate c1;
	update milstk set lstk_use = '0' where lstk_no = @lstk7; -- 금지건다

	RETURN @lp;
end
GO
PRINT N'프로시저 [dbo].[u_inpt_finish]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_inpt_finish]
	@gb int,
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);
	declare @fromlstk varchar(7);

	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare @dts varchar(19)
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts output
	set @iodate =substring(@dts, 1,10)
	set @iotime =substring(@dts, 12,8)
	
	declare @lp int = 0;

	if @gb = 1  set @fromlstk = 'Y000000';
	if @gb = 2  set @fromlstk = 'F000000';

	exec p_tilock;

	
	update milstk set lstk_io = '0', lstk_stat = '10' where lstk_no = @lstk7
	
	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;

	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,    @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
		if @@FETCH_STATUS <> 0 break;

		-- 이동이력 생성
		insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,           mvht_loc,     mvht_lot,
		                    mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
							mvht_to_lstk,  mvht_ioflag)
			    	values (@iodate,       @iotime,       @plti_prod,   @plti_pdesc,             @plti_loc,    @plti_lot, 
					        @plti_bestq,   @plti_remark,  @plti_pksz,   @plti_stok + @plti_rqty, @plti_pltno,  @fromlstk,
							@lstk7,       'I' )

		set @lp = @lp + 1
	end
	close c1;
	DEALLOCATE C1;

	RETURN @lp
end
GO
PRINT N'프로시저 [dbo].[u_move_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_move_cancel]
	@alstk varchar(7)
	
AS
begin
	update milstk set lstk_io = '0', lstk_stat = '10' where lstk_no = @alstk;

	RETURN 1
end
GO
PRINT N'프로시저 [dbo].[u_move_finish]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_move_finish]
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);
	declare @fromlstk varchar(7);

	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare @dts varchar(19)
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts output
	set @iodate =substring(@dts, 1,10)
	set @iotime =substring(@dts, 12,8)
	
	declare @ls_pltn varchar(8)
	declare @lp int = 0;

	
	exec p_tilock;

	declare @ls_dplt varchar(1)
	select @ls_dplt = stat_dplt from tbstat where stat_key = '1' 

	
	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;

	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,    @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
		if @@FETCH_STATUS <> 0 break;

		delete from miplti where current of c1;

		if @ls_dplt = '1' set @ls_pltn = '0000000'
		else set @ls_pltn = @pltno;

		if @plti_stok <> 0 begin
			update miplti set plti_stok = plti_stok + @plti_stok  -- 야적에 쓰고
			where plti_pltno = @ls_pltn
			and plti_lstk = 'Y000000'
			and plti_prod = @plti_prod
			and plti_loc = @plti_loc
			and plti_lot = @plti_lot
			and plti_bestq = @plti_bestq;
			if @@ROWCOUNT = 0 begin  -- 없으면 insert
				insert into miplti (plti_pltno,    plti_lstk,        plti_prod,    plti_pdesc,    plti_oprod,    plti_loc,   
									plti_lot,      plti_bestq,       plti_pksz,    plti_remark,   plti_icust,    plti_stok,   
									plti_rqty,     plti_cycl_date,   plti_idate,   plti_itime,    plti_flag,     plti_label)
						  values (  @ls_pltn,      'Y000000',        @plti_prod,   @plti_pdesc,   @plti_oprod,   @plti_loc,  
									@plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,  @plti_icust,   @plti_stok,
									0,             @plti_cycl_date,  @plti_idate,  @plti_itime,   '1',           '0' );

			end
		end
		-- 이동이력 생성
		insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,           mvht_loc,     mvht_lot,
							mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
							mvht_to_lstk,  mvht_ioflag)
		    		values (@iodate,       @iotime,       @plti_prod,   @plti_pdesc,             @plti_loc,    @plti_lot, 
							@plti_bestq,   @plti_remark,  @plti_pksz,   @plti_stok + @plti_rqty, @pltno,       @lstk7,
							'Y000000',     'M' )
	

		set @lp = @lp + 1
	end
	close c1;
	deallocate c1;

	update milstk set lstk_io = '0', lstk_stat = '00' where lstk_no = @lstk7  -- 빈셀만든다

	RETURN @lp
end
GO
PRINT N'프로시저 [dbo].[u_oupt_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_oupt_cancel]
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);
	declare @fromlstk varchar(7);

	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare @lp int = 0;
	declare @cc int = 0;
	declare @cc2 int = 0;
	

	exec p_tilock;

	declare @ls_dplt varchar(1)
	select @ls_dplt = stat_dplt from tbstat where stat_key = '1' 

	
	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;
		   
	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,    @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
		if @@FETCH_STATUS <> 0 break;
					
		update miplti set plti_stok = plti_stok + @plti_rqty, plti_rqty = 0  
		where plti_pltno = @pltno
		and plti_lstk = @lstk7
		and plti_prod = @plti_prod
		and plti_loc = @plti_loc
		and plti_lot = @plti_lot
		and plti_bestq = @plti_bestq;
	
		set @lp = @lp + 1
	end
	close c1;
	deallocate c1;

	update milstk set lstk_io = '0', lstk_stat = '10' where lstk_no = @lstk7  -- 재고상태

	-----------------------------------------------------------------------------------------------

	
	declare @docnum varchar(16)
	declare @sdno varchar(10)
	declare @posnr int = 0
	declare @ordxkey decimal
	declare @qty decimal
		
	while (1 > 0) begin

		select top 1 @ordxkey = ordxkey, @docnum = docnum, @sdno = sdno, @posnr = posnr, @qty = qty 
		from tiordx 
		where pltno = @pltno and lstk = @lstk7  and flag = '$X' 
		if @@ROWCOUNT = 0  break;
		
		delete from tiordx where ordxkey = @ordxkey;
		update miordi set rqty = rqty - @qty where docnum = @docnum and sdno = @sdno and posnr = @posnr;

	end

	declare @wmtxkey decimal
	declare @tanum int
	declare @tapos int
	
	while (1 > 0) begin

		select top 1 @wmtxkey = wmtxkey, @docnum = docnum, @tanum = tanum, @tapos = tapos, @qty = qty 
		from tiwmtx 
		where pltno = @pltno and lstk = @lstk7  and flag = '$X' 
		if @@ROWCOUNT = 0  break;
		
		delete from tiwmtx where wmtxkey = @wmtxkey;

		update miwmto set rqty = rqty - @qty where docnum = @docnum and tanum = @tanum and tapos = @tapos;

	end

	
	RETURN @lp
end
GO
PRINT N'프로시저 [dbo].[u_oupt_empt]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_oupt_empt]
	@aotype varchar(1),
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);
	declare @fromlstk varchar(7);

	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare @dts varchar(19)
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts output
	set @iodate =substring(@dts, 1,10)
	set @iotime =substring(@dts, 12,8)
	
	declare @hdate varchar(8)
	declare @htime varchar(6)
	set @hdate = substring(@dts, 1,4) + substring(@dts, 6,2) + substring(@dts, 9,2)
	set @htime = substring(@dts, 12,2) + substring(@dts, 15,2) + substring(@dts, 18,2)

	declare @ls_pltn varchar(8)
	declare @lp int = 0;
	declare @cc int = 0;
	declare @cc2 int = 0;
	

	exec p_tilock;

	declare @ls_dplt varchar(1)
	select @ls_dplt = stat_dplt from tbstat where stat_key = '1' 

	
	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;
		   
	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,    @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
		if @@FETCH_STATUS <> 0 break;

		--재고 삭제
		delete from miplti where current of c1;

		if @aotype = 'M' goto nn

		if @ls_dplt = '1' set @ls_pltn = '0000000'
		else set @ls_pltn = @pltno;

		-- 잔량이 있으면 야적에 생성
		if @plti_stok <> 0 begin
			update miplti set plti_stok = plti_stok + @plti_stok  -- 야적에 쓰고
			where plti_pltno = @ls_pltn
			and plti_lstk = 'Y000000'
			and plti_prod = @plti_prod
			and plti_loc = @plti_loc
			and plti_lot = @plti_lot
			and plti_bestq = @plti_bestq;
			if @@ROWCOUNT = 0 begin  -- 없으면 insert
				insert into miplti (plti_pltno,    plti_lstk,        plti_prod,    plti_pdesc,    plti_oprod,    plti_loc,   
									plti_lot,      plti_bestq,       plti_pksz,    plti_remark,   plti_icust,    plti_stok,   
									plti_rqty,     plti_cycl_date,   plti_idate,   plti_itime,    plti_flag,     plti_label)
						  values (  @ls_pltn,      'Y000000',        @plti_prod,   @plti_pdesc,   @plti_oprod,   @plti_loc,  
									@plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,  @plti_icust,   @plti_stok,
									0,             @plti_cycl_date,  @plti_idate,  @plti_itime,   '1',           '0' );

			end
				-- 이동이력 생성
			insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,           mvht_loc,     mvht_lot,
								mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
								mvht_to_lstk,  mvht_ioflag)
		    			values (@iodate,       @iotime,       @plti_prod,   @plti_pdesc,             @plti_loc,    @plti_lot, 
								@plti_bestq,   @plti_remark,  @plti_pksz,   @plti_stok,              @pltno,       @lstk7,
								'Y000000',     'M' )
		end
		-- 출고이력 생성
		insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,           mvht_loc,     mvht_lot,
							mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
							mvht_to_lstk,  mvht_ioflag)
		    		values (@iodate,       @iotime,       @plti_prod,   @plti_pdesc,             @plti_loc,    @plti_lot, 
							@plti_bestq,   @plti_remark,  @plti_pksz,   @plti_rqty,              @pltno,       @lstk7,
							'Z000000',     '$' )	
	nn:
		set @lp = @lp + 1
	end
	close c1;
	deallocate c1;

	update milstk set lstk_use = '0'/*, lstk_stat = '$E'*/ where lstk_no = @lstk7  -- 금지건다

	if @aotype = 'M' return @lp
	-----------------------------------------------------------------------------------------------


	declare @docnum varchar(16)
	declare @sdno varchar(10)
	declare @posnr int = 0
	declare @ordxkey decimal
	declare @qty decimal
		
	while (1 > 0) begin

		select top 1 @ordxkey = ordxkey, @docnum = docnum, @sdno = sdno, @posnr = posnr, @qty = qty 
		from tiordx 
		where pltno = @pltno and lstk = @lstk7  and flag = '$X' 
		if @@ROWCOUNT = 0  break;
		
		update tiordx set flag = '$Z' where ordxkey = @ordxkey;
		insert into hiordx select * from tiordx where ordxkey = @ordxkey;

		update miordi set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		update hiordi set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		if @@ROWCOUNT = 0 begin
			insert into hiordi select * from miordi where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		end

	end

	declare @wmtxkey decimal
	declare @tanum int
	declare @tapos int
	
	while (1 > 0) begin

		select top 1 @wmtxkey = wmtxkey, @docnum = docnum, @tanum = tanum, @tapos = tapos, @qty = qty 
		from tiwmtx 
		where pltno = @pltno and lstk = @lstk7  and flag = '$X' 
		if @@ROWCOUNT = 0  break;
		
		update tiwmtx set flag = '$Z' where wmtxkey = @wmtxkey;
		insert into hiwmtx select * from tiwmtx where wmtxkey = @wmtxkey;

		update miwmto set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		update hiwmto set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		if @@ROWCOUNT = 0 begin
			insert into hiwmto select * from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		end

	end

	
	RETURN @lp
end
GO
PRINT N'프로시저 [dbo].[u_oupt_finish]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_oupt_finish]
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);
	declare @fromlstk varchar(7);

	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare @dts19 varchar(19) = '';
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts19 output
	set @iodate =substring(@dts19, 1,10)
	set @iotime =substring(@dts19, 12,8)
	
	declare @hdate varchar(8)
	declare @htime varchar(6)
	set @hdate = substring(@dts19, 1,4) + substring(@dts19, 6,2) + substring(@dts19, 9,2)
	set @htime = substring(@dts19, 12,2) + substring(@dts19, 15,2) + substring(@dts19, 18,2)

	declare @ls_pltn varchar(8)
	declare @lp int = 0;
	declare @cc int = 0;
	declare @cc2 int = 0;
	

	exec p_tilock;

	declare @ls_dplt varchar(1)
	select @ls_dplt = stat_dplt from tbstat where stat_key = '1' 

	
	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;
		   
	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,    @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
		if @@FETCH_STATUS <> 0 break;

		--재고 삭제
		delete from miplti where current of c1;

		if @ls_dplt = '1' set @ls_pltn = '0000000'
		else set @ls_pltn = @pltno;

		-- 잔량이 있으면 야적에 생성
		if @plti_stok <> 0 begin
			update miplti set plti_stok = plti_stok + @plti_stok  -- 야적에 쓰고
			where plti_pltno = @ls_pltn
			and plti_lstk = 'Y000000'
			and plti_prod = @plti_prod
			and plti_loc = @plti_loc
			and plti_lot = @plti_lot
			and plti_bestq = @plti_bestq;
			if @@ROWCOUNT = 0 begin  -- 없으면 insert
				insert into miplti (plti_pltno,    plti_lstk,        plti_prod,    plti_pdesc,    plti_oprod,    plti_loc,   
									plti_lot,      plti_bestq,       plti_pksz,    plti_remark,   plti_icust,    plti_stok,   
									plti_rqty,     plti_cycl_date,   plti_idate,   plti_itime,    plti_flag,     plti_label)
						  values (  @ls_pltn,      'Y000000',        @plti_prod,   @plti_pdesc,   @plti_oprod,   @plti_loc,  
									@plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,  @plti_icust,   @plti_stok,
									0,             @plti_cycl_date,  @plti_idate,  @plti_itime,   '1',           '0' );

			end
				-- 이동이력 생성
			insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,           mvht_loc,     mvht_lot,
								mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
								mvht_to_lstk,  mvht_ioflag)
		    			values (@iodate,       @iotime,       @plti_prod,   @plti_pdesc,             @plti_loc,    @plti_lot, 
								@plti_bestq,   @plti_remark,  @plti_pksz,   @plti_stok,              @pltno,       @lstk7,
								'Y000000',     'M' )
		end
		-- 출고이력 생성
		insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,           mvht_loc,     mvht_lot,
							mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
							mvht_to_lstk,  mvht_ioflag)
		    		values (@iodate,       @iotime,       @plti_prod,   @plti_pdesc,             @plti_loc,    @plti_lot, 
							@plti_bestq,   @plti_remark,  @plti_pksz,   @plti_rqty,              @pltno,       @lstk7,
							'Z000000',     '$' )	

		set @lp = @lp + 1
	end
	close c1;
	deallocate c1;

	update milstk set lstk_io = '0', lstk_stat = '00' where lstk_no = @lstk7  -- 빈셀만든다

	-----------------------------------------------------------------------------------------------

	declare @docnum varchar(16)
	declare @sdno varchar(10)
	declare @posnr int = 0
	declare @ordxkey decimal
	declare @qty decimal
		
	while (1 > 0) begin

		select top 1 @ordxkey = ordxkey, @docnum = docnum, @sdno = sdno, @posnr = posnr, @qty = qty 
		from tiordx 
		where pltno = @pltno and lstk = @lstk7  and flag = '$X' 
		if @@ROWCOUNT = 0  break;
		
		update tiordx set flag = '$Z', credat = @hdate, cretim = @htime where ordxkey = @ordxkey;
		insert into hiordx select * from tiordx where ordxkey = @ordxkey;

		update miordi set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		update hiordi set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		if @@ROWCOUNT = 0 begin
			insert into hiordi select * from miordi where docnum = @docnum and sdno = @sdno and posnr = @posnr;
		end

	end

	declare @wmtxkey decimal
	declare @tanum int
	declare @tapos int
	
	while (1 > 0) begin

		select top 1 @wmtxkey = wmtxkey, @docnum = docnum, @tanum = tanum, @tapos = tapos, @qty = qty 
		from tiwmtx 
		where pltno = @pltno and lstk = @lstk7  and flag = '$X' 
		if @@ROWCOUNT = 0  break;
		
		update tiwmtx set flag = '$Z', credat = @hdate, cretim = @htime where wmtxkey = @wmtxkey;
		insert into hiwmtx select * from tiwmtx where wmtxkey = @wmtxkey;

		update miwmto set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		update hiwmto set fqty = fqty + @qty, hdate = @hdate, htime = @htime where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		if @@ROWCOUNT = 0 begin
			insert into hiwmto select * from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		end

	end

	RETURN @lp
end
GO
PRINT N'프로시저 [dbo].[u_ymove_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_ymove_cancel]
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	update miplti set plti_flag = '1' where plti_pltno = @apltn and plti_lstk = @alstk;
	if @@ROWCOUNT = 0 return -1 

	RETURN 1
end
GO
PRINT N'프로시저 [dbo].[u_ymove_finish]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[u_ymove_finish]
	@apltn varchar(8),
	@alstk varchar(7)
AS
begin
	
	declare @lstk7 varchar(7)= @alstk;
	declare @lstk6 varchar(6)= substring(@alstk, 2, 6);
	declare @pltno varchar(8) = @apltn;
	declare @dloca varchar(7);
	declare @fromlstk varchar(7);

	declare  @plti_pltno varchar(8)
	declare  @plti_lstk varchar(7)
	declare  @plti_prod varchar(18)
	declare  @plti_pdesc varchar(40)
	declare  @plti_oprod varchar(18)
	declare  @plti_loc varchar(4)
	declare  @plti_lot varchar(10)
	declare  @plti_bestq varchar(1)
	declare  @plti_pksz decimal(18,3)
	declare  @plti_remark varchar(40)
	declare  @plti_icust varchar(40)
	declare  @plti_stok decimal
	declare  @plti_rqty decimal
	declare  @plti_cycl_date varchar(10)
	declare  @plti_idate  varchar(10)
	declare  @plti_itime  varchar(8)
	declare  @plti_flag varchar(1)
	declare  @plti_label varchar(1)

	declare @dts varchar(19)
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts output
	set @iodate =substring(@dts, 1,10)
	set @iotime =substring(@dts, 12,8)
	
	declare @ls_pltn varchar(8)
	declare @lp int = 0;
	declare @fc int = 0;
	
	exec p_tilock;
	
	declare c1 cursor for
	select  plti_pltno,   
           plti_lstk,   
           plti_prod,   
           plti_pdesc,   
           plti_oprod,   
           plti_loc,   
           plti_lot,   
           plti_bestq,   
           plti_pksz,   
           plti_remark,   
           plti_icust,   
           plti_stok,   
           plti_rqty,   
           plti_cycl_date,   
           plti_idate,   
           plti_itime,   
           plti_flag,   
           plti_label from miplti where plti_pltno = @pltno and plti_lstk = @lstk7 for update;



	open c1;
	if @@ERROR <> 0 return -1

	while 1 > 0 begin
		fetch c1 into  @plti_pltno,   @plti_lstk,       @plti_prod,   @plti_pdesc,     @plti_oprod,   @plti_loc,  
		               @plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,    @plti_icust,   @plti_stok,
					   @plti_rqty,    @plti_cycl_date,  @plti_idate,  @plti_itime,     @plti_flag,    @plti_label;
					
		if  @@FETCH_STATUS <> 0 break;

		if @plti_lstk = 'F000000' begin
			delete from miplti where current of c1;
			
			update miplti set plti_stok = plti_stok + @plti_stok, plti_flag = '1' -- 야적에 쓰고
			where plti_pltno = @pltno
			and plti_lstk = 'Y000000'
			and plti_prod = @plti_prod
			and plti_loc = @plti_loc
			and plti_lot = @plti_lot
			and plti_bestq = @plti_bestq;
			if @@ROWCOUNT = 0 begin  -- 없으면 insert
				insert into miplti (plti_pltno,    plti_lstk,        plti_prod,    plti_pdesc,    plti_oprod,    plti_loc,   
									plti_lot,      plti_bestq,       plti_pksz,    plti_remark,   plti_icust,    plti_stok,   
									plti_rqty,     plti_cycl_date,   plti_idate,   plti_itime,    plti_flag,     plti_label)
						  values (  @pltno,        'Y000000',        @plti_prod,   @plti_pdesc,   @plti_oprod,   @plti_loc,  
									@plti_lot,     @plti_bestq,      @plti_pksz,   @plti_remark,  @plti_icust,   @plti_stok,
									0,             @plti_cycl_date,  @plti_idate,  @plti_itime,   '1',           '0' );

			end
		end		
		-- 이동이력 생성
		insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,           mvht_loc,     mvht_lot,
							mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
							mvht_to_lstk,  mvht_ioflag)
		    		values (@iodate,       @iotime,       @plti_prod,   @plti_pdesc,             @plti_loc,    @plti_lot, 
							@plti_bestq,   @plti_remark,  @plti_pksz,   @plti_stok,              @pltno,       @lstk7,
							'Y000000',     'M' )
	

		set @lp = @lp + 1
	end
	close c1;
	deallocate c1;
	
	RETURN @lp
end
GO
PRINT N'프로시저 [dbo].[p_changeloc]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_changeloc]
	@prod varchar(18),
	@ploc varchar(4),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@pltno varchar(8),
	@lstk varchar(7)
AS
begin
	
	declare @stok decimal = 0;
	declare @rqty decimal = 0;

	declare @rc int = 0;
	declare @hdate varchar(10)
	declare @htime varchar(8)
	declare @dts varchar(19) = ''
	
	exec @rc  =p_curgetdatetime19 @dts output
	if @rc <> 1 return -1
	set @hdate = substring(@dts, 1, 10);
	set @htime = substring(@dts, 12, 8);

	-- get old qty
	select @stok = plti_stok, @rqty = plti_rqty from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @ploc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -2

	-- change from 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime,
			plti_ctype,      plti_12 )  
	select plti_pltno,       plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'2',             '1'
	from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @ploc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -3


	-- new code qty
	update miplti set plti_stok = plti_stok +  @stok
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc   -- new
			and plti_lot = @lot
			and plti_bestq = @bestq
	if @@ROWCOUNT = 0 begin -- 
		begin try
			INSERT INTO miplti  
							( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
							  plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
							  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
							  plti_pdesc,      plti_oprod,  plti_icust )  
		  			select    plti_pltno,      plti_lstk,   plti_prod,       @loc,          plti_lot,   
							  plti_bestq,      plti_pksz,   plti_remark,     @stok,          0,
							  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     '0',
							  plti_pdesc,      plti_oprod,  plti_icust
					from miplti
					where plti_pltno = @pltno
					and plti_lstk = @lstk
					and plti_prod = @prod
					and plti_loc = @ploc  -- 기존
					and plti_lot = @lot
					and plti_bestq = @bestq
						
		end try
		begin catch
			return -99
		end catch
	end
	
	-- set old qty = 0 delete
	update miplti set plti_stok = 0
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @ploc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -4

		-- change to 기록 
	INSERT INTO mijchg  
			( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime,
			plti_ctype,      plti_12 )  
	select plti_pltno,       plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'2',             '2'
	from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @prod
		and plti_loc = @loc  -- new
		and plti_lot = @lot
		and plti_bestq = @bestq

	-- set old qty = 0  delete
	delete miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @ploc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_stok = 0
	  and plti_rqty = 0	 

	RETURN 1
END
GO
PRINT N'프로시저 [dbo].[p_changelot]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_changelot]
	@prod varchar(18),
	@loc varchar(4),
	@plot varchar(10),
	@lot varchar(10),
	@bestq varchar(1),
	@pltno varchar(8),
	@lstk varchar(7)
AS
begin
	
	declare @stok decimal = 0;
	declare @rqty decimal = 0;

	declare @rc int = 0;
	declare @hdate varchar(10)
	declare @htime varchar(8)
	declare @dts varchar(19) = ''
	
	exec @rc  =p_curgetdatetime19 @dts output
	if @rc <> 1 return -1
	set @hdate = substring(@dts, 1, 10);
	set @htime = substring(@dts, 12, 8);

	-- get old qty
	select @stok = plti_stok, @rqty = plti_rqty from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @plot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -2

	-- change from 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime,
			plti_ctype,      plti_12 )  
	select plti_pltno,       plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'3',             '1'
	from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @plot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -3


	-- new code qty
	update miplti set plti_stok = plti_stok +  @stok
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc   -- new
			and plti_lot = @lot
			and plti_bestq = @bestq
	if @@ROWCOUNT = 0 begin -- 
		begin try
			INSERT INTO miplti  
							( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
							  plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
							  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
							  plti_pdesc,      plti_oprod,  plti_icust )  
		  			select    plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      @lot,   -- new
							  plti_bestq,      plti_pksz,   plti_remark,     @stok,         0,
							  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     '0',
							  plti_pdesc,      plti_oprod,  plti_icust
					from miplti
					where plti_pltno = @pltno
					and plti_lstk = @lstk
					and plti_prod = @prod
					and plti_loc = @loc 
					and plti_lot = @plot  -- 기존
					and plti_bestq = @bestq					

		end try
		begin catch
			return -99
		end catch
	end

	--set old qty = 0 delete
	update miplti set plti_stok = 0
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @plot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -4

	-- change to 기록 
	INSERT INTO mijchg  
			( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime,
			plti_ctype,      plti_12 )  
	select plti_pltno,       plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime, 
			'3',             '2'
	from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @prod
		and plti_loc = @loc 
		and plti_lot = @lot
		and plti_bestq = @bestq

	-- set old qty = 0  delete
	delete miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @plot
	  and plti_bestq = @bestq
	  and plti_stok = 0
	  and plti_rqty = 0


	RETURN 1
END
GO
PRINT N'프로시저 [dbo].[p_changeprod]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_changeprod]
	@pprod varchar(18),
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@pltno varchar(8),
	@lstk varchar(7)
AS
begin
	
	declare @pksz decimal(18,3) = 0;
	declare @pdesc varchar(40) = '';
	declare @oprod varchar(18) = '';
	declare @stok decimal = 0;
	declare @rqty decimal = 0;

	declare @rc int = 0;
	declare @hdate varchar(10)
	declare @htime varchar(8)
	declare @dts varchar(19) = ''
	
	exec @rc  =p_curgetdatetime19 @dts output
	if @rc <> 1 return -1
	set @hdate = substring(@dts, 1, 10);
	set @htime = substring(@dts, 12, 8);

	select  @pdesc = mast_desc, @oprod = mast_old, @pksz = mast_vol from mimast where mast_cd = @prod
	if @@ROWCOUNT = 0 return -2

	-- get old qty
	select @stok = plti_stok, @rqty = plti_rqty from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @pprod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -3

	-- change from 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime, 
			plti_ctype,      plti_12 )  
	select plti_pltno,      plti_lstk,    plti_prod,        plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'1',             '1'
	from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @pprod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -4

	-- set old qty = 0 
	update miplti set plti_stok = 0
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @pprod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -5
	

	-- new code qty
	update miplti set plti_stok = plti_stok +  @stok
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @bestq
			and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 begin -- 
		begin try
			INSERT INTO miplti  
						( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
						  plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
						  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
						  plti_pdesc,      plti_oprod,  plti_icust )  
		  		select    plti_pltno,      plti_lstk,   @prod,           plti_loc,      plti_lot,   
						  plti_bestq,      @pksz,       plti_remark,     @stok,         0,
						  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     '0',
						  @pdesc,          @oprod,      plti_icust
				from miplti
				where plti_pltno = @pltno
				and plti_lstk = @lstk
				and plti_prod = @pprod  --기존
				and plti_loc = @loc
				and plti_lot = @lot
				and plti_bestq = @bestq
				and plti_rqty = 0 ;

		end try
		begin catch
			return -99
		end catch
	end
		-- change to 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime, 
			plti_ctype,      plti_12 )   
	select  plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     0,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'1',             '2'
	from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @prod  -- new
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @bestq		

	-- set old qty = 0  delete
	delete miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @pprod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq

	RETURN 1
END
GO
PRINT N'프로시저 [dbo].[p_changeqty]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_changeqty]
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@pltno varchar(8),
	@lstk varchar(7),
	@cqty decimal
AS
begin
	
	declare @stok decimal = 0;
	declare @rqty decimal = 0;

	declare @rc int = 0;
	declare @hdate varchar(10)
	declare @htime varchar(8)
	declare @dts varchar(19) = ''
	
	exec @rc  =p_curgetdatetime19 @dts output
	if @rc <> 1 return -1
	set @hdate = substring(@dts, 1, 10);
	set @htime = substring(@dts, 12, 8);

	-- get old qty
	select @stok = plti_stok from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -2

	-- change from 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime,
			plti_ctype,      plti_12 )  
	select plti_pltno,       plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,   
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'4',             '1'
	from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -3

	-- set new qty
	update miplti set plti_stok = @cqty
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -4

	-- change to 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime,
			plti_ctype,      plti_12 )  
	select plti_pltno,       plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,			
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'4',             '2'
	from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @prod
		and plti_loc = @loc  
		and plti_lot = @lot
		and plti_bestq = @bestq	
	
	-- qty = 0  delete
	delete miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_stok = 0;

	-- 20200605 changed
	--select @rc = count(*) from miplti where plti_lstk = @lstk
	--if @rc = 0 begin
	--	if SUBSTRING(@lstk,1,1) = 'A' begin
	--		update milstk set lstk_io = '0', lstk_stat = '00' where lstk_no = @lstk
	--	end
	--end

	RETURN 1
END
GO
PRINT N'프로시저 [dbo].[p_changeStatus]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_changeStatus]
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@nbestq varchar(1),
	@pltno varchar(8),
	@lstk varchar(7)
AS
begin
	
	declare @pksz decimal(18,3) = 0;
	declare @pdesc varchar(40) = '';
	declare @stok decimal = 0;
	declare @rqty decimal = 0;

	declare @rc int = 0;
	declare @hdate varchar(10)
	declare @htime varchar(8)
	declare @dts varchar(19) = ''
	
	exec @rc  =p_curgetdatetime19 @dts output
	if @rc <> 1 return -1
	set @hdate = substring(@dts, 1, 10);
	set @htime = substring(@dts, 12, 8);

	-- get old qty
	select @stok = plti_stok, @rqty = plti_rqty from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -3

	-- change from 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime, 
			plti_ctype,      plti_12 )  
	select plti_pltno,       plti_lstk,   plti_prod,        plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'5',             '1'
	from miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -4

	-- set old qty = 0 
	update miplti set plti_stok = 0
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq
	  and plti_flag = '1'
	  and plti_rqty = 0;
	if @@ROWCOUNT = 0 return -5
	

	-- new code qty
	update miplti set plti_stok = plti_stok +  @stok
		where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @nbestq
			and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 begin -- 
		begin try
			INSERT INTO miplti  
						( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
						  plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
						  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
						  plti_pdesc,      plti_oprod,  plti_icust )  
		  		select    plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
						  @nbestq,         plti_pksz,   plti_remark,     @stok,          0,
						  plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     '0',
						  plti_pdesc,      plti_oprod,  plti_icust
				from miplti
				where plti_pltno = @pltno
				and plti_lstk = @lstk
				and plti_prod = @prod  --기존
				and plti_loc = @loc
				and plti_lot = @lot
				and plti_bestq = @bestq
				and plti_rqty = 0 ;		
		end try
		begin catch
			return -99
		end catch
	end
		-- change to 기록 
	INSERT INTO mijchg  
		  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      plti_hdate,    plti_htime, 
			plti_ctype,      plti_12 )   
	select  plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
			plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     0,   
			plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
			plti_pdesc,      plti_oprod,  plti_icust,      @hdate,        @htime,
			'5',             '2'
	from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @prod 
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @nbestq			 

	-- set old qty = 0  delete
	delete miplti
	where plti_pltno = @pltno
	  and plti_lstk = @lstk
	  and plti_prod = @prod
	  and plti_loc = @loc
	  and plti_lot = @lot
	  and plti_bestq = @bestq

	RETURN 1
END
GO
PRINT N'프로시저 [dbo].[p_etc_cnfm_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_etc_cnfm_cancel]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@wmtxkey decimal,
	@pltno varchar(8),
	@lstk varchar(7),
	@matnr varchar(18),
	@maktx varchar(40),
	@lgort varchar(4),
	@charg varchar(10),
	@bestq varchar(1),
	@qty decimal,	

	@pksz decimal,
	@idate varchar(10),
	@itime varchar(8),	
	@oprod varchar(18),
	@remark varchar(40)		

AS
begin
	
	declare @stok decimal;
	-- lock ----
	exec p_tilock;
	
	delete from tiwmtx where wmtxkey = @wmtxkey and flag = '$Z';
	if @@ROWCOUNT = 0 return -1 ;
	delete from hiwmtx where wmtxkey = @wmtxkey and flag = '$Z';
		 
	update miplti 
	  set plti_stok = plti_stok + @qty
		where plti_pltno = '00000000' 
		  and plti_lstk = 'Y000000'
		  and plti_prod = @matnr
		  and plti_loc = @lgort
		  and plti_lot = @charg
		  and plti_bestq = @bestq;
	if @@ROWCOUNT = 0 
	begin	
		INSERT INTO miplti  
	       ( plti_pltno,    plti_lstk,        plti_prod,    plti_loc,         plti_lot,         
		     plti_bestq,    plti_pksz,        plti_remark,  plti_icust,       plti_stok, 
			 plti_rqty,     plti_cycl_date,   plti_idate,	plti_itime,       plti_flag,
			 plti_pdesc,    plti_label,       plti_oprod   )  
		values 
		   ( '00000000',    'Y000000',        @matnr,       @lgort,           @charg,
		     @bestq,         @pksz,           @remark,      '',               @qty,
		     0,              @idate,          @idate,       @itime,            '1',
			 @maktx,         '1',             @oprod );

	end

		
	update miwmto set rqty = rqty - @qty, fqty = fqty - @qty
	where  docnum = @docnum
	and    tanum = @tanum
	and    tapos = @tapos
	
	update hiwmto set fqty = fqty - @qty
	where  docnum = @docnum
	and    tanum = @tanum
	and    tapos = @tapos

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_etc_exec_spec]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_etc_exec_spec]  -- 실행시는 반드시 lstk로 sort바람
	@docnum varchar(16), 
	@tanum decimal,
	@tapos int,
	@bwlvs varchar(3),
	@pltno varchar(8),
	@lstk varchar(7),
	@matnr varchar(18),
	@lgort varchar(4),
	@charg varchar(10),
	@bestq varchar(1),
	@qty decimal,
	
	@pksz decimal,
	@idate varchar(10),
	@itime varchar(8),	
	@oprod varchar(18),
	@remark varchar(40)		

AS
begin
	-- lock ----
	exec p_tilock;

	declare @flag varchar(2) = '';
	declare @plstk varchar(7) = '';

	
	declare @dts varchar(14) ='';
	exec p_curgetdatetime14 @dts output;
	declare @odate varchar(8);
	declare @otime varchar(6);

	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);
	
	declare @hogi varchar(1) = '';
	declare @fstn varchar(2) = '';
	declare @tstn varchar(2) = '';
	declare @jno varchar(18) = '';
	declare @indx varchar(4) = '';
	declare @rc int = 0;

	declare @matnrdesc varchar(40) = ''

	declare @dts19 varchar(19)
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts19 output
	set @iodate =substring(@dts19, 1,10)
	set @iotime =substring(@dts19, 12,8)

	if substring(@lstk, 1, 1) = 'A' begin
		set @flag = '$X';

		if @lstk <> @plstk begin 
			update milstk set lstk_io = '$', lstk_stat = '$X'  where lstk_no = @lstk and lstk_io in ('', '0') and lstk_stat = '10' ;
			if @@ROWCOUNT = 0 return -1; --Rack상태변함

			exec @rc = p_get_indx_jno '3', @jno output;
			
			set @indx = right(@jno, 4)
			exec p_get_hogi @lstk, @hogi output;	
			
			select  @fstn = right('00' + cast(convert(int, @hogi) * 2 as varchar(2)), 2);
			set @tstn = '43';

			INSERT INTO tbindx  
  	      	     (  indx_jno,      indx_indx,       indx_gubn,        indx_jio,        indx_hogi,   
  		      	    indx_fstn,     indx_tstn,       indx_pltn,        indx_lstk,       indx_xmov,   
     		      	 indx_edat,    indx_sflg,       indx_uflg )  
		   values  ( @jno,         @indx,           'A',              '$',             @hogi,
			         @fstn,        @tstn,           @pltno,           @lstk,           '$',
					 '',           'W',             '0'       ) ;
			
			set @plstk = @lstk;  -- save 
		end
	
		update miplti 
			  set plti_stok = plti_stok - @qty, 
			      plti_rqty = plti_rqty + @qty
			where plti_pltno = @pltno 
			  and plti_lstk = @lstk
			  and plti_prod = @matnr
			  and plti_loc = @lgort
			  and plti_lot = @charg
			  and plti_bestq = @bestq
		if @@ROWCOUNT = 0 return -2; --재고 상태변함
	
		INSERT INTO tiwmtx  
		 			 ( docnum,  tanum,  tapos, bwlvs,   IO,  lstk,   pltno,   qty,    flag,  pksz,   credat,  cretim,   remark, idate,  itime,  oprod )  
		     VALUES ( @docnum, @tanum, @tapos, @bwlvs, '$',  @lstk,  @pltno,  @qty,   '$X',  @pksz,  @odate,  @otime,   @remark, @idate, @itime, @oprod) ;
			   
		update miwmto set rqty = rqty + @qty
		where  docnum = @docnum
		  and  tanum = @tanum
		  and  tapos = @tapos;		


	end			
	else begin
		set @flag = '$Z';
		
		update miplti 
			  set plti_stok = plti_stok - @qty
			where plti_pltno = @pltno 
			  and plti_lstk = @lstk
			  and plti_prod = @matnr
			  and plti_loc = @lgort
			  and plti_lot = @charg
			  and plti_bestq = @bestq
		if @@ROWCOUNT = 0 return -3; --야적재고 상태변함
			
		delete from miplti
			where plti_pltno = @pltno 
			  and plti_lstk = @lstk
			  and plti_prod = @matnr
			  and plti_loc = @lgort
			  and plti_lot = @charg
			  and plti_bestq = @bestq
			  and plti_stok = 0
			  and plti_rqty = 0;
		
		INSERT INTO tiwmtx  
		 			 ( docnum,  tanum,  tapos, bwlvs,   IO,  lstk,   pltno,   qty,    flag,  pksz,   credat,  cretim,   remark, idate,  itime,  oprod )  
		     VALUES ( @docnum, @tanum, @tapos, @bwlvs, '$',  @lstk,  @pltno,  @qty,   '$Z',  @pksz,  @odate,  @otime,   @remark, @idate, @itime, @oprod) ;

		INSERT INTO hiwmtx  select * from tiwmtx where docnum = @docnum and tanum = @tanum and tapos = @tapos;
				
		update miwmto set rqty = rqty + @qty, fqty = fqty + @qty, hdate = @odate, htime = @otime
		  where  docnum = @docnum
			and  tanum = @tanum
			and  tapos = @tapos

		update hiwmto set fqty = fqty + @qty, hdate = @odate, htime = @otime
		  where  docnum = @docnum
			and  tanum = @tanum
			and  tapos = @tapos
		if @@ROWCOUNT = 0 begin
			insert into hiwmto select * from miwmto where docnum = @docnum and  tanum = @tanum and tapos = @tapos	
		end

		-- 이동이력 생성
		select @matnrdesc = mast_cd from mimast where mast_cd = @matnr;
		insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,       mvht_loc,     mvht_lot,
							mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,              mvht_pltno,   mvht_from_lstk, 
							mvht_to_lstk,  mvht_ioflag)
		    		values (@iodate,       @iotime,       @matnr,       @matnrdesc,              @lgort,       @charg, 
							'',            @remark,       @pksz,        @qty,                    @pltno,       @lstk,
							'Z000000',     '$' )



	end
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_etc_out_exec]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_etc_out_exec]-- 실행시는 반드시 lstk로 sort바람
AS
begin
	
	declare @wmtxkey decimal;
	declare @docnum varchar(16);
	declare @tanum decimal
	declare @tapos int;
	declare @pltno varchar(8);
	declare @lstk varchar(7);
	declare @oqty decimal;
	declare @matnr varchar(18)
	declare @maktx varchar(40)
	declare @remark varchar(40)

	declare @lgort varchar(4);
	declare @charg varchar(10);
	declare @bestq varchar(1)
	declare @pksz decimal(13,3);

	declare @plstk varchar(7) = '';
	declare @flag varchar(2) = '';
	declare @jno varchar(18) = '';
	declare @rc int = 0 ;
	declare @indx varchar(4) ;
	declare @hogi varchar(1) ;
	declare @fstn varchar(2) ;
	declare @tstn varchar(2) ;
	declare @lp int = 0 ;

	declare @dts varchar(14) = '' ;
	exec p_curgetdatetime14 @dts output;
	declare @hdate varchar(8) = substring( @dts, 1,8)
	declare @htime varchar(6) = substring( @dts, 9,6)

	declare @dts19 varchar(19)
	declare @iodate varchar(10)
	declare @iotime varchar(8)

	exec p_curgetdatetime19 @dts19 output
	set @iodate =substring(@dts19, 1,10)
	set @iotime =substring(@dts19, 12,8)
	
	exec p_tilock

	declare c1 cursor for
	select b.wmtxkey, a.docnum, a.tanum, a.tapos, b.pltno, b.lstk, b.qty, a.matnr, a.lgort, a.charg, b.pksz, a.maktx, b.remark, a.bestq
	  from miwmto a (updlock), tiwmtx b (updlock) 
	where a.docnum = b.docnum
	and a.tanum = b.tanum
	and a.tapos = b.tapos
	and b.flag = '$R' order by b.lstk; 

	open c1;

	while(1>0) begin

		fetch c1 into @wmtxkey, @docnum, @tanum, @tapos, @pltno, @lstk, @oqty, @matnr, @lgort, @charg, @pksz, @maktx, @remark, @bestq;
		if @@FETCH_STATUS <> 0 break;

		if substring(@lstk,1,1) = 'A' begin
			set @flag = '$X';			
			if (@lstk <> @plstk) begin
				update milstk set lstk_io = '$', lstk_stat = '$X'	where lstk_no = @lstk and lstk_stat = '$R'

				exec @rc = p_get_indx_jno '3',  @jno output
				set @indx = Right(@jno, 4);

				exec p_get_hogi @lstk, @hogi output;

				set @fstn = right('00' + cast(CONVERT(int, @hogi) * 2 as varchar(2)), 2);			
				set @tstn = '43';

				INSERT INTO tbindx  
  	      			  ( indx_jno,     indx_indx,       indx_gubn,        indx_jio,        indx_hogi,   
  		      			indx_fstn,    indx_tstn,       indx_pltn,        indx_lstk,       indx_xmov,   
     					indx_edat,    indx_sflg,       indx_uflg )  
				values  ( @jno,         @indx,           'A',              '$',             @hogi,
					     @fstn,        @tstn,           @pltno,           @lstk,           '$',
						 '',           'W',             '0'       ) ;
				set @plstk = @lstk;
			end 
			update tiwmtx set flag = @flag where wmtxkey = @wmtxkey;

		end else begin -- 자동창고가 아닌경우
			set @flag = '$Z';
			
			update miplti set plti_rqty = plti_rqty - @oqty
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq = @bestq
			
			delete from miplti
			  where plti_pltno = @pltno
		  	    and plti_lstk = @lstk
			    and plti_prod = @matnr
			    and plti_loc = @lgort
			    and plti_lot = @charg
			    and plti_bestq = @bestq
				and plti_stok = 0
				and plti_rqty = 0;

			update miwmto set fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and tanum = @tanum
				and tapos = @tapos;		
			update hiwmto set fqty = fqty + @oqty, hdate = @hdate, htime = @htime
				where docnum = @docnum
				and tanum = @tanum
				and tapos = @tapos;		
			if @@ROWCOUNT = 0 begin
				insert into hiwmto select * from miwmto where docnum = @docnum	and tanum = @tanum and tapos = @tapos;	
			end		

			update tiwmtx set flag = @flag where wmtxkey = @wmtxkey;
			insert into hiwmtx select * from tiwmtx where wmtxkey = @wmtxkey;

			-- 이동이력 생성
			insert into mimvht (mvht_io_date,  mvht_io_time,  mvht_prod,    mvht_proddesc,       mvht_loc,     mvht_lot,
								mvht_bestq,    mvht_remark,   mvht_pksz,    mvht_ioqty,          mvht_pltno,   mvht_from_lstk, 
								mvht_to_lstk,  mvht_ioflag)
		    			values (@iodate,       @iotime,       @matnr,       @maktx,               @lgort,       @charg, 
								@bestq,        @remark,       @pksz,        @oqty,                @pltno,       @lstk,
								'Z000000',     '$' )	

		end
		
		set @lp = @lp + 1;
		
	end
	close c1;
	deallocate c1;

	RETURN  @lp;
end
GO
PRINT N'프로시저 [dbo].[p_etc_rsrv_uline]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_etc_rsrv_uline]  -- 실행시는 반드시 lstk로 sort바람
	@docnum varchar(16), 
	@tanum decimal, 
	@tapos int,
	@matnr varchar(18), 
	@lgort varchar(4), 
	@charg varchar(10),
	@bestq varchar(1)	
AS
begin
	declare @oq   decimal
	declare @rq decimal;
	declare @sq decimal;
	declare @oqty decimal;

	declare @date varchar(8);
	declare @time varchar(6);

	declare @canqty int = 1;
	
	
	declare @ho1 varchar(1) = '1';
	declare @ho2 varchar(1) = '2';
	declare @ho3 varchar(1) = '3';
	declare @ho4 varchar(1) = '4';
	declare @ho5 varchar(1) = '5';
	declare @scrc_gbun varchar(1);
	declare @scrc_onln varchar(1);
	declare @scrc_emer varchar(1);
	declare @scrc_ouse varchar(1);
	declare @scrc_comm varchar(1);

	declare @dumy int;
	declare @pltno varchar(8);
	declare @loca varchar(7);
	declare @pstok decimal;
	declare @prq decimal;
	declare @pksz decimal(18,3);
	declare @remark varchar(40);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);

	declare @maktx varchar(40);
	declare @bwlvs varchar(1);
	declare @oprod varchar(18);

	
	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '01';
	if @scrc_ouse = '0' set @ho1 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '02';
	if @scrc_ouse = '0' set @ho2 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '03';
	if @scrc_ouse = '0' set @ho3 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '04';
	if @scrc_ouse = '0' set @ho4 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '05';
	if @scrc_ouse = '0' set @ho5 = '9';
		 
	declare @rc int = 0;
	declare @lp int = 0;
	declare @dts varchar(14) = '';

	declare @odate varchar(8);
	declare @otime varchar(6);
	
	exec @rc = p_curgetdatetime14 @dts output;	
	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);

	-- lock ----
	exec p_tilock;
	
	declare c1 cursor for
    SELECT matnr, charg, lgort, vsolm, rqty
      FROM miwmto   
	where docnum = @docnum
	  and tanum = @tanum
	  and tapos = @tapos
	  and matnr = @matnr
	  and lgort = @lgort
	  and charg = @charg
	  and io = '$'
	  and vsolm - rqty > 0 ;

	open c1;
	while 1 > 0 begin
		fetch c1 into @matnr,@charg,@lgort,@oq, @rq;
		if @@FETCH_STATUS <> 0 break;

		set @sq = @oq - @rq;		
		while @sq > 0 begin
		
			Select top 1 
				@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
				@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
			from v_rsrv 
			where  plti_prod = @matnr
			and    plti_loc = @lgort
			and    plti_lot = @charg
			and    plti_bestq = ''
			and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) order by 1, 3, 8, 9 ;
			if @@ROWCOUNT = 0 break;		

			if substring(@loca, 1, 1) = 'A' begin
				update milstk set lstk_io = '$', lstk_stat = '$R'  where lstk_no = @loca ;
			end

			if @sq > @pstok  begin --large order so fetch again
				update miplti set plti_stok = plti_stok - @pstok, plti_rqty = plti_rqty + @pstok
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
			
				set @oqty = @pstok;
				set @sq = @sq - @pstok;
			end
			else begin     -- large plti to scr again
				update miplti set plti_stok = plti_stok - @sq, plti_rqty = plti_rqty + @sq
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
		
				set @oqty = @sq;
				set @sq = 0; 
			end

			INSERT INTO tiwmtx  
		 			 ( docnum,  tanum,  tapos,  lstk,   pltno,   qty,    flag,  pksz, credat,  cretim,   remark, idate,  itime,  oprod )  
		      VALUES ( @docnum, @tanum, @tapos, @loca,  @pltno,  @oqty, '$R',  @pksz, @odate,  @otime,  @remark, @idate, @itime, @oprod) ;

  
			update miwmto set rqty = rqty + @oqty
			where  docnum = @docnum
			and    tanum = @tanum
			and    tapos = @tapos
			and    vsolm - rqty > 0 ;
				
			set @lp = @lp + 1;
		end
		
	end
	close c1;
	deallocate c1;

	RETURN @lp;
end
GO
PRINT N'프로시저 [dbo].[p_inptexec]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_inptexec]
	@pltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@fstn varchar(2),
	@fygubun  varchar(1)    -- 1: 공장 2:메인입고대
AS
begin
	
	--if igb = 'F' then
	--	loca  = 'F000000'
	--	lsi   = '1'
	--	fstn = '22'
	--else
	--	loca  = 'Y000000'
	--	lsi   = '2'
	--	fstn = '21'
	--end if	
	declare @rc int = 0;
	declare @cc int;
	declare @ls_type varchar(1);
	declare @ls_lstk varchar(6) = '';

	declare @dtstr varchar(19) = '';
	declare @idate varchar(10) = '';
	declare @itime varchar(8) = '';
	declare @lhno int = 0;
	declare @ls_hogi char(1);
	declare @imode varchar(1);
	
	exec p_tilock;

	select @cc = count(*) from miplti where plti_pltno = @pltno and plti_lstk = @lstk
	 if @cc = 0 return -1;  -- 상태변함

	select @cc = count(*) from miplti where plti_pltno = @pltno and plti_lstk = @lstk and plti_rqty > 0;
	 if @cc > 0 return -2;    -- 출고예약이 있음
	
	select @ls_type = mast_flag from mimast where mast_cd = @prod;  -- 제품코드 등록바람
	if @@Rowcount = 0 return -3;

	 -- 바코드 입고모드이므로 파렛트 선택입고 불가...!!
	select @imode = stat_imode from tbstat where stat_key = '1'
	if @imode = '1' return -4;

	--빈셀 할당 
	if @ls_type = '0' exec  @rc = p_get_rsrv_hogi '0', @ls_lstk output;
	if @ls_type = '1' exec  @rc = p_get_rsrv_hogi1 '1', @ls_lstk output;
	if @ls_type = '2' exec  @rc = p_get_rsrv_hogi2 '2', @ls_lstk output;
	if @ls_type = '3' exec  @rc = p_get_rsrv_hogi3 '3', @ls_lstk output;
	if @rc <> 1 return -55;
	if @ls_lstk = '' return -5;  -- 빈셀없음

	declare @alstk varchar(7);

	set @alstk = 'A' + @ls_lstk;
	
	update milstk set lstk_io = 'I', lstk_stat = 'IX' where lstk_no = @alstk and lstk_io = '0';
	if @@ROWCOUNT = 0 return -6; -- 보관위치 상태변함

	exec @rc = p_curgetdatetime19 @dtstr OUTPUT;
	if @rc <> 1 return -7;  -- 시간얻기 실패

	set @idate = SUBSTRING(@dtstr, 1,10);
	set @itime = SUBSTRING(@dtstr, 12,8);

	update miplti set plti_lstk = @alstk,
	                  plti_idate = @idate,
					  plti_cycl_date = @idate,
					  plti_itime = @itime 
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_rqty = 0;
	if @@ROWCOUNT = 0 or @@ERROR <> 0 return -8; -- 재고상태 상태변함2

	declare @tstn char(2) = '';
	if SUBSTRING(@ls_lstk, 1,2) = '01' or SUBSTRING(@ls_lstk, 1,2) = '02'  set @tstn = '01';
	if SUBSTRING(@ls_lstk, 1,2) = '03' or SUBSTRING(@ls_lstk, 1,2) = '04'  set @tstn = '03';
	if SUBSTRING(@ls_lstk, 1,2) = '05' or SUBSTRING(@ls_lstk, 1,2) = '06'  set @tstn = '05';
	if SUBSTRING(@ls_lstk, 1,2) = '07' or SUBSTRING(@ls_lstk, 1,2) = '08'  set @tstn = '07';
	if SUBSTRING(@ls_lstk, 1,2) = '09' or SUBSTRING(@ls_lstk, 1,2) = '10'  set @tstn = '09';

	set @lhno = (CONVERT(int, @tstn) + 1) / 2;
	set @ls_hogi = @lhno;
	
	declare @jno varchar(18);
	declare @indx varchar(4);

	set @rc = 0;
	exec @rc = p_get_indx_jno @fygubun, @jno output;  -- '1' : 공장동 '2':메인입고
	if @rc <> 1 return -9;  -- 작업번호 얻기 실패

	set @indx = right(@jno, 4);

	INSERT INTO tbindx  
   	       ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
      	     indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
             indx_edat,     indx_sflg,       indx_uflg )  
	  VALUES ( @jno,          @indx,           'A',             'I',           @ls_hogi,
   	           @fstn,         @tstn,           @pltno,          @alstk,        'I',
			  '',            'P',             '0');

	RETURN 1; 
end
GO
PRINT N'프로시저 [dbo].[P_miwmto_in2]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[P_miwmto_in2]
	@docnum varchar(16),
	@tanum  decimal,
	@tapos  int,
	@qty decimal
AS
begin
	declare @hdt varchar(19);
	declare @idate varchar(10);
	declare @itime varchar(8);

	declare @hdate varchar(8);
	declare @htime varchar(6);

	declare @prod   varchar(18);
	declare @oprod   varchar(18);
	declare @pdesc  varchar(40);
	declare @loc    varchar(4);
	declare	@lot    varchar(10);
	declare @vsolm   decimal;
	declare @pksz   decimal(18,3);
	declare @pksz2   decimal(18,3);
	declare @bwlvs varchar(3);
	declare @loca varchar(7);

	begin try

		exec p_tilock

		select @prod = matnr, @pdesc =maktx, @loc = lgort, @lot = charg, @vsolm = vsolm, @bwlvs = bwlvs, @pksz = pksz 
		from miwmto  
		where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		if @@ROWCOUNT = 0 return -1;

		update hiwmto set vsolm = vsolm + @qty where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		if @@ROWCOUNT = 0 begin
			insert into hiwmto select * from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos;

			update hiwmto set vsolm = @qty where docnum = @docnum and tanum = @tanum and tapos = @tapos;
			if @@ROWCOUNT = 0 return -2;
		end
		
		select @pdesc = mast_desc1 from mimast where mast_cd = @prod
		if @@ROWCOUNT = 0 return -6;

		update miwmto set vsolm = vsolm - @qty where docnum = @docnum and tanum = @tanum and tapos = @tapos;
		delete from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos and vsolm <= 0;
	
		select @hdt = convert(char(19), getdate(), 121) from tbstat;
		select @hdate = substring(@hdt, 1,4)  + substring(@hdt, 6,2) +  substring(@hdt, 9,2);
		select @htime = substring(@hdt, 12,2) +  substring(@hdt, 15,2) +  substring(@hdt, 18,2);
	
		update hiwmto set hdate = @hdate, htime = @htime where docnum = @docnum and tanum = @tanum and tapos = @tapos;

		select @idate = substring(@hdt, 1,4) + '/' +substring(@hdt, 6,2) + '/' + substring(@hdt, 9,2);
		select @itime = substring(@hdt, 12,2) + ':' + substring(@hdt, 15,2) + ':' + substring(@hdt, 18,2);
		
		--if @bwlvs = '101' set @loca = 'F000000'
		--else  	
		
		if (@pksz = 0) begin
			select @pksz = mast_vol from mimast where mast_cd = @prod;
			if @@ROWCOUNT = 0 begin
				RETURN -4;
			end
		end

		set @loca = 'Y000000'

		update miplti set plti_stok = plti_stok + @qty 
		where plti_pltno = '00000000' 
		  and plti_lstk = @loca
		  and plti_prod = @prod 
		  and plti_loc = @loc
		  and plti_lot = @lot
		  and plti_bestq = '';
		if @@ROWCOUNT = 0 begin	
			insert into miplti (plti_pltno, plti_lstk,  plti_prod,   plti_pdesc,     plti_loc,   plti_lot,  plti_bestq, 
								plti_pksz,  plti_stok,  plti_rqty,   plti_cycl_date, plti_idate, plti_itime, plti_remark, 
								plti_flag,  plti_icust, plti_label,  plti_oprod)
			values (           '00000000',  @loca,      @prod,       @pdesc,         @loc,       @lot,       '',
								@pksz,      @qty,      0,            @idate,         @idate,     @itime,     '',
								'1',        '',        '0',          '' );
		end
	end try
	begin catch
		return -99;
	end catch					          
	  	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_movepltno_fy_yf]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_movepltno_fy_yf]
	@pltno varchar(8),
	@lstk varchar(7),
	@fstn varchar(2)
AS
begin
	--if igb = 'F' then
	--	loca  = 'F000000'
	--	fstn = '22'
	--else
	--	loca = 'Y000000'
	--	fstn = '21'
	--end if	

	exec p_tilock;

	declare @cc int = 0;
	declare @imode char(1) = '';

	--상태변함
	select @cc = count(*) from miplti where plti_pltno = @pltno and plti_lstk  = @lstk;
	if @cc = 0 return -1;

	-- 출고예약 되어 있음
	select @cc = count(*) from miplti where plti_pltno = @pltno and plti_lstk  = @lstk and plti_rqty > 0;
	if @cc > 0 return -2;

	-- 순환이동중
	select @cc = count(*) from miplti where plti_pltno = @pltno and plti_lstk  = @lstk and plti_flag = 'N';
	if @cc > 0 return -3;
 
    -- 바코드 입고모드이므로 파렛트 선택입고 불가...!!
	select @imode = stat_imode from tbstat where stat_key = '1'
	if @imode = '1' return -4;

	-- --상태변함 update
	update miplti set plti_flag = 'N' where plti_pltno = @pltno  and plti_lstk = @lstk;
	if @@ROWCOUNT = 0 return -5;
	
	declare @jno varchar(18);
	declare @indx varchar(4);
	declare @rc int = 0;
	
	exec @rc = p_get_indx_jno '4', @jno output
	if @rc <> 1 return -5;

	set @indx = RIGHT(@jno, 4);

	
	INSERT INTO tbindx  
		     ( indx_jno,      indx_indx,       indx_gubn,       indx_jio,      indx_hogi,   
		       indx_fstn,     indx_tstn,       indx_pltn,       indx_lstk,     indx_xmov,   
		       indx_edat,     indx_sflg,       indx_uflg )  
	 VALUES ( @jno,          @indx,           'A',             'M',           '0',
			  @fstn,         '43',            @pltno,          @lstk,         'N',
			  '',            'P',             '0');

	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_out_cnfm_cancel]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_out_cnfm_cancel]
	@docnum varchar(16),
	@sdno varchar(10),
	@posnr int,
	@ordxkey decimal,
	@pltno varchar(8),
	@lstk varchar(7),
	@oqty decimal,
	@pksz decimal(18,3),
	@remark varchar(40),
	@idate varchar(10),
	@itime varchar(8),
	@oprod varchar(18)

AS
begin

declare @lc int = 0;


	declare @matnr varchar(18)
	declare @matnrdesc varchar(40)
	declare @lgort varchar(4)
	declare @charg varchar(10)

	exec p_tilock;

	select @matnr= matnr, @matnrdesc = matnrdesc, @lgort = lgort, @charg = charg 
	from miordi where docnum = @docnum and sdno = @sdno  and posnr = @posnr;
	if @@ROWCOUNT = 0 return -1;  -- 상태변함 miordi

	delete from tiordx where ordxkey = @ordxkey and flag = '$Z';
	delete from hiordx where ordxkey = @ordxkey and flag = '$Z';


	if @@ROWCOUNT = 0 return -2;  -- 상태변함 tiordx

	update miplti set plti_stok = plti_stok + @oqty, plti_rqty = plti_rqty - @oqty
		where plti_pltno = '00000000'
		  and plti_lstk  = 'Y000000'
   		  and plti_prod = @matnr
		  and plti_loc = @lgort
		  and plti_lot = @charg
		  and plti_bestq = ''
	if @@ROWCOUNT = 0 begin
		INSERT INTO miplti  
			   ( plti_pltno,      plti_lstk,     plti_prod,        plti_loc,         plti_lot,      plti_bestq,      
				 plti_pksz,       plti_remark,   plti_icust,       plti_stok,        plti_rqty,     plti_cycl_date,  
				 plti_idate,      plti_itime,    plti_flag,        plti_oprod,       plti_pdesc,    plti_label )
		values ( '00000000',      'Y000000',     @matnr,           @lgort,           @charg,         '',
				 @pksz,           @remark,       '',               @oqty,            0,             @idate, 
				 @idate,          @itime,        '1',              @oprod,           @matnrdesc,    '0' );
	end

	update miordi set rqty = rqty - @oqty,  fqty = fqty - @oqty
	where  docnum = @docnum
	and    sdno  = @sdno
	and    posnr  = @posnr;
	if @@ROWCOUNT = 0 return -2;  -- 상태변함 miordi
	
	update hiordi set fqty = fqty - @oqty
	where  docnum = @docnum
	and    sdno  = @sdno
	and    posnr  = @posnr;


RETURN 1
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_bestq_spec2]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_bestq_spec2]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@bwlvs varchar(3),
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@bestq2 varchar(1),
	@cqty decimal, 
	@pltno varchar(8),
	@uqty decimal = 0 output
	
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0
	declare @cc  int = 0

	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	set  @uqty = 0
	exec p_tilock;

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	--check
	select @cc = count(*) from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos and vsolm - fqty = @cqty;
	if @cc <= 0 return -1

	select @lstk = plti_lstk, @stok = plti_stok , @pksz = plti_pksz,  @pdesc = plti_pdesc, @remark = plti_remark, @idate = plti_idate,  @itime = plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0')
	if @@ROWCOUNT = 0 return -2

	if @cqty > @stok begin					
		set @uqty = @stok;
		set @cqty = @cqty - @stok;
	end else begin
	    set @uqty = @cqty;
		set @cqty = 0
	end

	update miplti set plti_stok = plti_stok - @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 return -3

	select @cc = Count(*)  
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq2
	  and (a.plti_rqty > 0 or b.lstk_io not in ('', '0'));
    if @cc > 0 return -4

	update miplti set plti_stok = plti_stok +  @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq2
	if @@ROWCOUNT = 0 begin
			
		INSERT INTO miplti  
					( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )  
		values (    @pltno,          @lstk,       @matnr,          @lgort,        @charg,
					@bestq2,         @pksz,       @remark,         @uqty,         0,
					@idate,          @idate,      @itime,          '1',           '0',
					@pdesc,          '',          '' );                  
					 
	end			
				
	insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	           	values (@docnum, @tanum, @tapos + 1, @bwlvs, 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음
			
	insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
	delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0 return -3;
	
	update hiwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = @bwlvs
		   if @@ROWCOUNT = 0 return -100;
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = @bwlvs
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_charg_spec2]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_charg_spec2]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@bwlvs varchar(3),
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@charg2 varchar(10),
	@cqty decimal, 
	@pltno varchar(8),
	@uqty decimal = 0 output
	
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0
	declare @cc  int = 0

	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	set @uqty = 0
	
	exec p_tilock;

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

		--check
	select @cc = count(*) from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos and vsolm - fqty = @cqty;
	if @cc <= 0 return -1

	select @lstk = plti_lstk, @stok = plti_stok , @pksz = plti_pksz,  @pdesc = plti_pdesc, @remark = plti_remark, @idate = plti_idate,  @itime = plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0')
	if @@ROWCOUNT = 0 return-2

	if @cqty > @stok begin					
		set @uqty = @stok;
		set @cqty = @cqty - @stok;
	end else begin
	    set @uqty = @cqty;
		set @cqty = 0
	end

	update miplti set plti_stok = plti_stok - @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 return -3

	select @cc = Count(*)  
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg2
	  and a.plti_bestq = @bestq
	  and (a.plti_rqty > 0 or b.lstk_io not in ('', '0'));
    if @cc > 0 return -4

	update miplti set plti_stok = plti_stok +  @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg2
		and plti_bestq = @bestq
	if @@ROWCOUNT = 0 begin
			
		INSERT INTO miplti  
					( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )  
		values (    @pltno,          @lstk,       @matnr,          @lgort,        @charg2,
					@bestq,          @pksz,       @remark,         @uqty,         0,
					@idate,          @idate,      @itime,          '1',           '0',
					@pdesc,          '',          '' );                  
					 
	end			
				
	insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	           	values (@docnum, @tanum, @tapos + 1, @bwlvs, 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음
			
	insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
	delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0  return -3;
	
	update hiwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = @bwlvs
		  if  @@ROWCOUNT = 0 return -100
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = @bwlvs
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_lgort_spec]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_lgort_spec]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@lgort2 varchar(4),
	@cqty decimal, 
	@pltno varchar(8)
	
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0
	declare @cc int = 0;

	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)

	declare @uqty decimal

	exec p_tilock;

	--check
	select @cc = count(*) from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos and vsolm - rqty = @cqty;
	if @cc <= 0 return-1

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	select @lstk = plti_lstk, @stok = plti_stok , @pksz = plti_pksz,  @pdesc = plti_pdesc, @remark = plti_remark, @idate = plti_idate,  @itime = plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0')
	if @@ROWCOUNT = 0 return-1

	if @cqty > @stok begin					
		set @uqty = @stok;
		set @cqty = @cqty - @stok;
	end else begin
	    set @uqty = @cqty;
		set @cqty = 0
	end

	update miplti set plti_stok = plti_stok - @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 return -1

	update miplti set plti_stok = plti_stok +  @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort2
		and plti_lot = @charg
		and plti_bestq = @bestq
	if @@ROWCOUNT = 0 begin
			
		INSERT INTO miplti  
					( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )  
		values (    @pltno,          @lstk,       @matnr,          @lgort2,       @charg,
					@bestq,          @pksz,       @remark,         @uqty,         0,
					@idate,          @idate,      @itime,          '1',           '0',
					@pdesc,          '',          '' );                  
					 
	end			
				
	insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	           	values (@docnum, @tanum, @tapos + 1, '309', 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음
			
	insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
	delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0 return -3;
	
	update hiwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = '309'
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = '309'
		  if  @@ROWCOUNT = 0 return -100
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = '309'
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltichng_lgort_spec2]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltichng_lgort_spec2]
	@docnum varchar(16),
	@tanum decimal,
	@tapos int,
	@bwlvs varchar(3),
	@matnr varchar(18),
    @lgort varchar(4),
	@charg varchar(10),
    @bestq varchar(1),
	@lgort2 varchar(4),
	@cqty decimal, 
	@pltno varchar(8),
	@uqty decimal = 0 output
	
AS
begin
	declare @rc int = 0;
	declare @ret int = 0;
	declare @cnt  int = 0
	declare @cc int = 0;

	declare @lstk varchar(7);
	declare @stok decimal;
	declare @remark varchar(40);
	declare @date varchar(8);
	declare @time varchar(6);
	declare @dts varchar(14) = '';
	declare @pksz decimal(18,3)
	declare @pdesc varchar(40)
	declare @idate varchar(10)
	declare @itime varchar(8)
	declare @oprod varchar(18)
	
	set @uqty = 0;	

	exec p_tilock;

	--check
	select @cc = count(*) from miwmto where docnum = @docnum and tanum = @tanum and tapos = @tapos and vsolm - fqty = @cqty;
	if @cc <= 0 return-1

	exec @rc = p_curgetdatetime14 @dts output;
	set @date = substring(@dts, 1, 8);
	set @time = substring(@dts, 9, 6);

	select @lstk = plti_lstk, @stok = plti_stok , @pksz = plti_pksz,  @pdesc = plti_pdesc, @remark = plti_remark, @idate = plti_idate,  @itime = plti_itime
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and a.plti_flag = '1'
	  and a.plti_rqty = 0 
	  and b.lstk_io in ('', '0')
	if @@ROWCOUNT = 0 return-2

	if @cqty > @stok begin					
		set @uqty = @stok;
		set @cqty = @cqty - @stok;
	end else begin
	    set @uqty = @cqty;
		set @cqty = 0
	end

	update miplti set plti_stok = plti_stok - @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_rqty = 0 ;
	if @@ROWCOUNT = 0 return -3
	
	select @cc = Count(*)  
	from miplti a, milstk b 
	where a.plti_lstk = b.lstk_no   
	  and a.plti_pltno = @pltno
	  and a.plti_prod = @matnr
	  and a.plti_loc = @lgort2
	  and a.plti_lot = @charg
	  and a.plti_bestq = @bestq
	  and (a.plti_rqty > 0 or b.lstk_io not in ('', '0'));
    if @cc > 0 return -4

	update miplti set plti_stok = plti_stok +  @uqty
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort2
		and plti_lot = @charg
		and plti_bestq = @bestq
	if @@ROWCOUNT = 0 begin
			
		INSERT INTO miplti  
					( plti_pltno,    plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )  
		values (    @pltno,          @lstk,       @matnr,          @lgort2,       @charg,
					@bestq,          @pksz,       @remark,         @uqty,         0,
					@idate,          @idate,      @itime,          '1',           '0',
					@pdesc,          '',          '' );                  
					 
	end			
				
	insert into tiwmtx (docnum,  tanum, tapos,       bwlvs, IO, lstk, pltno, qty, flag, credat, cretim, remark)
	           	values (@docnum, @tanum, @tapos + 1, @bwlvs, 'C', @lstk, @pltno, @uqty, '$Z', @date, @time, @remark); -- flag관련없음
			
	insert into hiwmtx select * from tiwmtx where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
	delete from tiwmtx  where docnum = @docnum and tanum = @tanum  and tapos = @tapos + 1;
		
	delete from miplti
	where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @matnr
		and plti_loc = @lgort
		and plti_lot = @charg
		and plti_bestq = @bestq
		and plti_flag = '1'
		and plti_stok = 0 
		and plti_rqty = 0 ;

	update miwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0 return -3;
	
	update hiwmto set fqty = fqty + @uqty , hdate = @date, htime = @time
		where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1,2)
		  and bwlvs = @bwlvs
	if @@ROWCOUNT = 0  begin
		 insert into hiwmto select * from miwmto  
		 where docnum = @docnum
		  and tanum = @tanum
		  and tapos in (1, 2)
		  and bwlvs = @bwlvs
		  if  @@ROWCOUNT = 0 return -100
	end		
	 
	delete from miwmto 
	  where docnum = @docnum
	  and tanum = @tanum
	  and tapos in (1, 2)
	  and bwlvs = @bwlvs
	  and fqty >= vsolm ;
	
	RETURN 1;
end
GO
PRINT N'프로시저 [dbo].[p_pltzadd_n]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltzadd_n]
	@pltno varchar(8),
	@npltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@stok decimal,
	@sqty decimal,
	@labelyn int,
	@prnno varchar(1)  -- F 공장 프린터 번호 2  Y:메인 프린터 번호 1
AS
begin
	declare @pksz decimal(18,3);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @pdesc varchar(40);
	declare @canqty int;
	declare @mlabel varchar(1);
	declare @remark varchar(40);
	declare @oprod varchar(18);

	declare @dts varchar(19);
	declare @cnt int = 0;
	declare @pltcnt int = 0;
	declare @sumqty decimal = 0;

	exec p_tilock;

	select @canqty = mast_canqty from mimast where mast_cd = @prod;
	if @@ROWCOUNT = 0 return -1;

	-- step 상태첵크
	select @cnt = count(*) from miplti where plti_pltno = @npltno and plti_lstk = @lstk;
	if @cnt = 0 return -2;
	
	select @pdesc = plti_pdesc, @pksz = plti_pksz, @idate = plti_idate, @remark = plti_remark, @oprod = plti_oprod from miplti
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq
	and   plti_stok = @stok
	and   plti_stok >= @sqty
	and   plti_rqty = 0;
	if @@ROWCOUNT = 0 return -3;

	select @pdesc = mast_desc1 from mimast where mast_cd = @prod
	if @@ROWCOUNT = 0 return -6;

	update miplti set plti_stok = plti_stok - @sqty
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq ;
	if @@ROWCOUNT = 0 return -4;

	
	select @dts = convert(varchar(19), getdate(), 121) from tbstat;
	set @cdate = substring(@dts, 1,4) + '/' + substring(@dts, 6,2) + '/' + substring(@dts, 9,2);
	set @idate = @cdate;
	set @itime =  substring(@dts, 12,2) + ':' + substring(@dts, 15,2) + ':' + substring(@dts, 18,2);

	if @labelyn = 1 set @mlabel  = '1';
	else set @mlabel  = '0';
		
	begin try
		
		update miplti set plti_stok = plti_stok + @sqty
		where plti_pltno = @npltno
		and plti_lstk = @lstk
		and plti_prod = @prod
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @bestq ;
		if @@ROWCOUNT = 0 begin
			INSERT INTO miplti  
				  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )
		    select  @npltno,         plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     @sqty,          0,   
					@cdate,          @idate,      @itime,          '1',           @mlabel,
					plti_pdesc,      plti_oprod,  plti_icust  
			from miplti
			where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @bestq;	
			if @@ROWCOUNT = 0 return -99;

		end
		
		update miplti set plti_label = '1' where plti_pltno = @npltno and plti_lstk = @lstk ; --혼적 할수 있으므로

		select @pltcnt = count(*), @sumqty = sum(plti_stok) from miplti where plti_pltno = @npltno;
		
		if @labelyn = 1 begin
	
			--if SUBSTRING(@lstk,1,1) = 'F' set @prnno = '2';
			--else set @prnno = '1';

			if @pltcnt = 1 begin
				INSERT INTO tbbprn  
  		  			  		(prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
				values     ( @prnno,   @npltno,        @prod,     @pdesc,     @lot,      @pksz,      @sumqty,     1,            GETDATE() );
			end
			else begin
				INSERT INTO tbbprn  
  		  			  		(prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
				values     ( @prnno,   @npltno,        '',       '',         '',        0.00,       @sumqty,     @pltcnt,      GETDATE() );
			end
		end
		
	end try
	begin catch 
		return -999;
	end catch;

	delete from miplti 	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq 
	and plti_stok = 0
	and plti_rqty = 0; 

	RETURN 1;
END
GO
PRINT N'프로시저 [dbo].[p_pltzerall_n]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltzerall_n]
	@pltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@stok decimal,
	@sqty decimal,
	@labelyn int,
	@prnno varchar(1)  -- F 공장 프린터 번호 2  Y:메인 프린터 번호 1
AS
begin
	declare @pksz decimal(18,3) = 0;
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @pdesc varchar(40);
	declare @canqty decimal = 0;
	declare @mlabel varchar(1);
	declare @remark varchar(40);
	declare @oprod varchar(18);

	declare @dts varchar(19);
	declare @npltno varchar(8);

	select @canqty = mast_canqty from mimast where mast_cd = @prod;
	if @@ROWCOUNT = 0 return -1; -- 제품코드등록바람
	if @canqty = 0 set @canqty = 1;

	if @sqty <= 0 return -2;     -- 선택수량 없음
	if @stok < @sqty return -3;  -- 선택수량 너무큼

	exec p_tilock;

	-- step 상태첵크
	
	select @pdesc = plti_pdesc, @pksz = plti_pksz, @idate = plti_idate, @remark = plti_remark, @oprod = plti_oprod from miplti
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq
	and   plti_stok = @stok
	and   plti_rqty = 0;
	if @@ROWCOUNT = 0 return -4;	 -- 상태변함

	select @pdesc = mast_desc1 from mimast where mast_cd = @prod
	if @@ROWCOUNT = 0 return -6;

	while (@sqty > 0) begin
		if @sqty > @canqty set  @sqty = @sqty - @canqty;
		else begin
			set @canqty = @sqty;
			set @sqty = 0;
		end

		update miplti set plti_stok = plti_stok - @canqty
		where plti_pltno = @pltno
		and plti_lstk = @lstk
		and plti_prod = @prod
		and plti_loc = @loc
		and plti_lot = @lot
		and plti_bestq = @bestq ;
		if @@ROWCOUNT = 0 return -5;  -- 상태변함2

		declare @rc int = 0;
		exec @rc = p_getpltno @npltno output;
		if @rc = 0 return -6          -- 파렛번호 얻기 실패
		if len(@npltno) <> 8 return -7;  -- 파렛번호 얻기 실패2

		select @dts = convert(varchar(19), getdate(), 121) from tbstat;
		set @cdate = substring(@dts, 1,4) + '/' + substring(@dts, 6,2) + '/' + substring(@dts, 9,2);
		set @idate = @cdate;
		set @itime =  substring(@dts, 12,2) + ':' + substring(@dts, 15,2) + ':' + substring(@dts, 18,2);

		if @labelyn = 1 set @mlabel  = '1';
		else set @mlabel  = '0';
		
		begin try
		
			INSERT INTO miplti  
					 ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					   plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					   plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					   plti_pdesc,      plti_oprod,  plti_icust )  
			values (   @npltno,         @lstk,       @prod,           @loc,          @lot,
					   @bestq,          @pksz,       @remark,         @canqty,       0,
					   @cdate,          @idate,      @itime,          '1',           @mlabel,
					   @pdesc,          @oprod,      '' );                  
		
		
			if @labelyn = 1 begin
	
				--if SUBSTRING(@lstk,1,1) = 'F' set @prnno = '2';
				--else set @prnno = '1';

				INSERT INTO tbbprn  
  		  		  		   (prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
				values    ( @prnno,   @npltno,       @prod,     @pdesc,     @lot,      @pksz,      @canqty,   1,            GETDATE() );
			end
		
		end try
		begin catch 
			return -999;   -- 파렛번호 이미 발행
		end catch;
	end --end while

	delete from miplti 	
	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq 
	and plti_stok = 0
	and plti_rqty = 0; 

	RETURN 1;
END
GO
PRINT N'프로시저 [dbo].[p_pltznew_n]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_pltznew_n] 
	@pltno varchar(8),
	@lstk varchar(7),
	@prod varchar(18),
	@loc varchar(4),
	@lot varchar(10),
	@bestq varchar(1),
	@stok decimal,
	@sqty decimal,
	@labelyn int,
	@prnno varchar(1)  -- F 공장 프린터 번호 2  Y:메인 프린터 번호 1
AS
begin
	declare @pksz decimal(18,3);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @pdesc varchar(40);
	declare @canqty int;
	declare @mlabel varchar(1);
	declare @remark varchar(40);
	declare @oprod varchar(18);

	declare @dts varchar(19);
	declare @npltno varchar(8);

	exec p_tilock;

	select @canqty = mast_canqty from mimast where mast_cd = @prod;
	if @@ROWCOUNT = 0 return -1;

	-- step 상태첵크	
	select @pdesc = plti_pdesc, @pksz = plti_pksz, @idate = plti_idate, @remark = plti_remark, @oprod = plti_oprod from miplti
	where plti_pltno = @pltno
	and   plti_lstk = @lstk
	and   plti_prod = @prod
	and   plti_loc = @loc
	and   plti_lot = @lot
	and   plti_bestq = @bestq
	and   plti_stok = @stok
	and   plti_stok >= @sqty
	and   plti_rqty = 0;
	if @@ROWCOUNT = 0 return -2;
	
	select @pdesc = mast_desc1 from mimast where mast_cd = @prod
	if @@ROWCOUNT = 0 return -6;

	update miplti set plti_stok = plti_stok - @sqty
	where plti_pltno = @pltno
	and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq ;
	if @@ROWCOUNT = 0 return -3;

	declare @rc int  = 0;
	exec @rc = p_getpltno @npltno output;
	if @rc <> 1 return -4
	if len(@npltno) <> 8 return -5;

	select @dts = convert(varchar(19), getdate(), 121) from tbstat;
	set @cdate = substring(@dts, 1,4) + '/' + substring(@dts, 6,2) + '/' + substring(@dts, 9,2);
	set @idate = @cdate;
	set @itime =  substring(@dts, 12,2) + ':' + substring(@dts, 15,2) + ':' + substring(@dts, 18,2);

	if @labelyn = 1 set @mlabel  = '1'
	else set @mlabel  = '0';
		
	begin try
		
		INSERT INTO miplti  
				  ( plti_pltno,      plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     plti_stok,     plti_rqty,   
					plti_cycl_date,  plti_idate,  plti_itime,      plti_flag,     plti_label,
					plti_pdesc,      plti_oprod,  plti_icust )
		    select  @npltno,         plti_lstk,   plti_prod,       plti_loc,      plti_lot,   
					plti_bestq,      plti_pksz,   plti_remark,     @sqty,          0,   
					@cdate,          @idate,      @itime,          '1',           @mlabel,
					plti_pdesc,      plti_oprod,  plti_icust  
			from miplti
			where plti_pltno = @pltno
			and plti_lstk = @lstk
			and plti_prod = @prod
			and plti_loc = @loc
			and plti_lot = @lot
			and plti_bestq = @bestq;	
			if @@ROWCOUNT = 0 return -99;
		
		if @labelyn = 1 begin
	
			--if SUBSTRING(@lstk,1,1) = 'F' set @prnno = '2'
			--else set @prnno = '1';

			INSERT INTO tbbprn  
  		  		  		(prn_no,   prn_pltno,     prn_prod,  prn_pdesc,  prn_lot,   prn_pksz,   prn_qty,   prn_mixcnt,   prn_date )  
			values     ( @prnno,   @npltno,        @prod,     @pdesc,     @lot,      @pksz,      @sqty,     1,            GETDATE() )
		end
		
	end try
	begin catch 
		return -999
	end catch;

	delete from miplti 	where plti_pltno = @pltno and plti_lstk = @lstk
	and plti_prod = @prod
	and plti_loc = @loc
	and plti_lot = @lot
	and plti_bestq = @bestq 
	and plti_stok = 0
	and plti_rqty = 0; 

	RETURN 1;
END
GO
PRINT N'프로시저 [dbo].[p_rsrv_cust]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_rsrv_cust]
	@cust varchar(17),
	@credat varchar(8)
	
	
AS
begin
	declare @wecust varchar(17);
	declare @docnum varchar(16);
	declare @sdno varchar(10);
	declare @posnr int;
	declare @matnr varchar(18);
	declare @charg varchar(10);
	declare @lgort varchar(4);
	declare @oq decimal;
	declare @rq decimal;
	declare @sq decimal;
	declare @oqty decimal;

	declare @date varchar(8);
	declare @time varchar(6);

	declare @canqty int = 1;
	
	
	declare @ho1 varchar(1) = '1';
	declare @ho2 varchar(1) = '2';
	declare @ho3 varchar(1) = '3';
	declare @ho4 varchar(1) = '4';
	declare @ho5 varchar(1) = '5';
	declare @scrc_gbun varchar(1);
	declare @scrc_onln varchar(1);
	declare @scrc_emer varchar(1);
	declare @scrc_ouse varchar(1);
	declare @scrc_comm varchar(1);

	declare @dumy int;
	declare @pltno varchar(8);
	declare @loca varchar(7);
	declare @pstok decimal;
	declare @prq decimal;
	declare @pksz decimal;
	declare @remark varchar(40);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	declare @oprod varchar(18);

	declare @odate varchar(10);
	declare @otime varchar(8);

	--select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	--from tbscrc where  scrc_no = '01';
	--if @scrc_ouse = '0' set @ho1 = '9';

	--select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	--from tbscrc where  scrc_no = '02';
	--if @scrc_ouse = '0' set @ho2 = '9';

	--select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	--from tbscrc where  scrc_no = '03';
	--if @scrc_ouse = '0' set @ho3 = '9';

	--select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	--from tbscrc where  scrc_no = '04';
	--if @scrc_ouse = '0' set @ho4 = '9';

	--select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	--from tbscrc where  scrc_no = '05';
	--if @scrc_ouse = '0' set @ho5 = '9';
		 
	declare @rc int = 0;
	declare @lp int = 0;
	declare @dts varchar(14) = '';

	exec @rc = p_curgetdatetime14 @dts output;	
	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);

	-- lock ----
	exec p_tilock;
	
	declare c1 cursor for
    SELECT wecust, docnum, sdno, posnr, matnr, charg, lgort, qty, rqty
      FROM miordi   
	where credat = @credat
	  and cust = @cust 
	  and qty - rqty > 0
	  and flag <> '2' 
	order by 1, 5, 6, 7;

	open c1;
	while 1 > 0 begin
		fetch c1 into @wecust,@docnum,@sdno,@posnr,@matnr,@charg,@lgort,@oq,@rq;
		if @@FETCH_STATUS <> 0 break;

		select @canqty = mast_canqty from mimast where mast_cd = @matnr;
		
		set @sq = @oq - @rq;		
		while @sq > 0 begin

			if @sq > @canqty begin

				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk, @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) 	
				and    plti_stok >= @canqty  ORDER BY 1, 4 desc, 8, 9 ;
				if @@ROWCOUNT = 0 begin
					Select top 1 
						@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk, 
						@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
					from v_rsrv 
					where  plti_prod = @matnr
					and    plti_loc = @lgort
					and    plti_lot = @charg
					and    plti_bestq = ''
					and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) order by 1, 4 desc, 3, 8, 9 ;
					if @@ROWCOUNT = 0 break;
				end
			end 
			else begin
				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk, 
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) order by 1, 4 desc, 3, 8, 9 ;
				if @@ROWCOUNT = 0 break;
			end

			if substring(@loca, 1, 1) = 'A' begin
				update milstk set lstk_io = '$', lstk_stat = '$R'  where lstk_no = @loca ;
			end

			if @sq > @pstok  begin --large order so fetch again
				update miplti set plti_stok = plti_stok - @pstok, plti_rqty = plti_rqty + @pstok
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
			
				set @oqty = @pstok;
				set @sq = @sq - @pstok;
			end
			else begin     -- large plti to scr again
				update miplti set plti_stok = plti_stok - @sq, plti_rqty = plti_rqty + @sq
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
		
				set @oqty = @sq;
				set @sq = 0; 
			end

			INSERT INTO tiordx  
		 			 ( docnum,  sdno,  posnr,  lstk,   pltno,   qty,    flag,  pksz,  credat,  cretim,   remark , idate, itime,  oprod)  
		      VALUES ( @docnum, @sdno, @posnr, @loca,  @pltno,  @oqty, '$R',   @pksz,  @odate,  @otime,  @remark, @idate, @itime,  @oprod) ;

  
			update miordi set rqty = rqty + @oqty
			where  docnum = @docnum
			and    sdno = @sdno
			and    posnr = @posnr
			and    qty - rqty > 0 ;
				
			set @lp = @lp + 1;
		end
		
	end
	close c1;
	deallocate c1;

	RETURN @lp;
end
GO
PRINT N'프로시저 [dbo].[p_rsrv_doc]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_rsrv_doc]
	@doc varchar(16),  -- 
	@credat varchar(8)
	
	
AS
begin
	declare @wecust varchar(17);
	declare @sdno varchar(10);
	declare @docnum varchar(16);
	declare @posnr int;
	declare @matnr varchar(18);
	declare @charg varchar(10);
	declare @lgort varchar(4);
	declare @oq decimal;
	declare @rq decimal;
	declare @sq decimal;
	declare @oqty decimal;

	declare @odate varchar(8);
	declare @otime varchar(6);

	declare @canqty int = 1;
	
	
	declare @ho1 varchar(1) = '1';
	declare @ho2 varchar(1) = '2';
	declare @ho3 varchar(1) = '3';
	declare @ho4 varchar(1) = '4';
	declare @ho5 varchar(1) = '5';
	declare @scrc_gbun varchar(1);
	declare @scrc_onln varchar(1);
	declare @scrc_emer varchar(1);
	declare @scrc_ouse varchar(1);
	declare @scrc_comm varchar(1);

	declare @dumy int;
	declare @pltno varchar(8);
	declare @loca varchar(7);
	declare @pstok decimal;
	declare @prq decimal;
	declare @pksz decimal;
	declare @remark varchar(40);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);
	
	declare @oprod varchar(18);

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '01';
	if @scrc_ouse = '0' set @ho1 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '02';
	if @scrc_ouse = '0' set @ho2 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '03';
	if @scrc_ouse = '0' set @ho3 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '04';
	if @scrc_ouse = '0' set @ho4 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '05';
	if @scrc_ouse = '0' set @ho5 = '9';
		 
	declare @rc int = 0;
	declare @lp int = 0;
	declare @dts varchar(14) = '';

	exec @rc = p_curgetdatetime14 @dts output;	
	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);

	-- lock ----
	exec p_tilock;
	
	declare c1 cursor for
    SELECT wecust, docnum, sdno, posnr, matnr, charg, lgort, qty, rqty
      FROM miordi   
	where credat = @credat
	  and docnum = @doc
	  and qty - rqty > 0
	  and flag <> '2' 
	order by 1, 5, 6, 7;

	open c1;
	while 1 > 0 begin
		fetch c1 into @wecust,@docnum, @sdno, @posnr,@matnr,@charg,@lgort,@oq,@rq;
		if @@FETCH_STATUS <> 0 break;

		select @canqty = mast_canqty from mimast where mast_cd = @matnr;
		
		set @sq = @oq - @rq;		
		while @sq > 0 begin

			if @sq > @canqty begin

				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk, @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) 	
				and    plti_stok >= @canqty  ORDER BY 1, 4 desc, 8, 9 ;
				if @@ROWCOUNT = 0 begin
					Select top 1 
						@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk, @oprod = plti_oprod,
						@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
					from v_rsrv 
					where  plti_prod = @matnr
					and    plti_loc = @lgort
					and    plti_lot = @charg
					and    plti_bestq = ''
					and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) order by 1, 4 desc, 3, 8, 9 ;
					if @@ROWCOUNT = 0 break;
				end
			end 
			else begin
				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk, @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv 
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) order by 1, 4 desc, 3, 8, 9 ;
				if @@ROWCOUNT = 0 break;
			end

			if substring(@loca, 1, 1) = 'A' begin
				update milstk set lstk_io = '$', lstk_stat = '$R'  where lstk_no = @loca ;
			end

			if @sq > @pstok  begin --large order so fetch again
				update miplti set plti_stok = plti_stok - @pstok, plti_rqty = plti_rqty + @pstok
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
			
				set @oqty = @pstok;
				set @sq = @sq - @pstok;
			end
			else begin     -- large plti to scr again
				update miplti set plti_stok = plti_stok - @sq, plti_rqty = plti_rqty + @sq
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
		
				set @oqty = @sq;
				set @sq = 0; 
			end

			INSERT INTO tiordx  
		 			 ( docnum,  sdno,  posnr,  lstk,   pltno,   qty,    flag,  pksz,  credat,  cretim,  remark,  idate,  itime, oprod )  
		      VALUES ( @docnum, @sdno, @posnr, @loca,  @pltno,  @oqty, '$R',  @pksz,  @odate,  @otime,  @remark, @idate, @itime, @oprod) ;

  
			update miordi set rqty = rqty + @oqty
			where  docnum = @docnum
			and    sdno = @sdno
			and    posnr = @posnr
			and    qty - rqty > 0 ;
				
			set @lp = @lp + 1;
		end
		
	end
	close c1;
	deallocate c1;

	RETURN @lp;
end
GO
PRINT N'프로시저 [dbo].[p_rsrv_order]을(를) 만드는 중...';


GO
CREATE PROCEDURE [dbo].[p_rsrv_order]
	@orderno varchar(10),  -- sdno
	@credat varchar(8)	
	
AS
begin
	declare @wecust varchar(17);
	declare @sdno varchar(10);
	declare @docnum varchar(16);
	declare @posnr int;
	declare @matnr varchar(18);
	declare @charg varchar(10);
	declare @lgort varchar(4);
	declare @oq decimal;
	declare @rq decimal;
	declare @sq decimal;
	declare @oqty decimal = 0;
	declare @toqty decimal = 0;

	declare @date varchar(8);
	declare @time varchar(6);

	declare @canqty int = 1;
	
	
	declare @ho1 varchar(1) = '1';
	declare @ho2 varchar(1) = '2';
	declare @ho3 varchar(1) = '3';
	declare @ho4 varchar(1) = '4';
	declare @ho5 varchar(1) = '5';
	declare @scrc_gbun varchar(1);
	declare @scrc_onln varchar(1);
	declare @scrc_emer varchar(1);
	declare @scrc_ouse varchar(1);
	declare @scrc_comm varchar(1);

	declare @dumy int;
	declare @pltno varchar(8);
	declare @loca varchar(7);
	declare @pstok decimal;
	declare @prq decimal;
	declare @pksz decimal(18,3);
	declare @remark varchar(40);
	declare @cdate varchar(10);
	declare @idate varchar(10);
	declare @itime varchar(8);

	declare @oprod varchar(18);

	
	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '01';
	if @scrc_ouse = '0' set @ho1 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '02';
	if @scrc_ouse = '0' set @ho2 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '03';
	if @scrc_ouse = '0' set @ho3 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '04';
	if @scrc_ouse = '0' set @ho4 = '9';

	select @scrc_gbun = scrc_gubn,  @scrc_onln = scrc_onln, @scrc_emer = scrc_emer,  @scrc_ouse = scrc_ouse,  @scrc_comm = scrc_comm
	from tbscrc where  scrc_no = '05';
	if @scrc_ouse = '0' set @ho5 = '9';
		 
	declare @rc int = 0;
	declare @lp int = 0;
	declare @dts varchar(14) = '';

	declare @odate varchar(8);
	declare @otime varchar(6);
	declare @fail int = 0;

	exec @rc = p_curgetdatetime14 @dts output;	
	set @odate = substring(@dts, 1,8);
	set @otime = substring(@dts, 9,6);

	-- lock ----
	exec p_tilock;
	
	declare c1 cursor static for
    SELECT wecust, docnum, sdno, posnr, matnr, charg, lgort, qty, rqty
      FROM miordi with(updlock)  
	where sdno = @orderno
	  and qty - rqty > 0 
	order by  5, 7, 6, 3 ;
	--order by 1, 5, 6, 8 ;

	open c1;
	if @@CURSOR_ROWS <= 0 return @lp;
	 
	while 1 > 0 begin
		fetch c1 into @wecust,@docnum, @sdno, @posnr,@matnr,@charg,@lgort,@oq,@rq;
		if @@FETCH_STATUS <> 0 break;

		set @toqty =0;	
		set @oqty =0;	
		
		set @canqty = 0;
		select @canqty = mast_canqty from mimast where mast_cd = @matnr;		

		set @sq = @oq - @rq;		
		while @sq > 0 begin		
			if @canqty <= 0 set @canqty = 1;

			if @sq >= @canqty begin			    

				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv with(updlock)
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) 	
				and    plti_stok >= @canqty  ORDER BY 1, 4 desc, 9, 10 ;
				if @@ROWCOUNT = 0 begin
					Select top 1 
						@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
						@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
					from v_rsrv  with(updlock)
					where  plti_prod = @matnr
					and    plti_loc = @lgort
					and    plti_lot = @charg
					and    plti_bestq = ''
					and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) order by 1, 4 desc, 3, 9, 10 ;
					if @@ROWCOUNT = 0 break;
				end
			end 
			else begin
				Select top 1 
					@dumy = dumy, @pltno = plti_pltno, @pstok = plti_stok, @prq = plti_rqty, @loca = plti_lstk,  @oprod = plti_oprod,
					@pksz = plti_pksz, @cdate = plti_cycl_date, @idate = plti_idate, @itime = plti_itime, @remark = plti_remark   				   
				from v_rsrv  with(updlock)
				where  plti_prod = @matnr
				and    plti_loc = @lgort
				and    plti_lot = @charg
				and    plti_bestq = ''
				and    lstk_hogi in ( @ho1, @ho2, @ho3, @ho4, @ho5,'0' ) order by 1, 4 desc, 3, 9, 10 ;
				if @@ROWCOUNT = 0 break;
			end

			if @sq > @pstok  begin --large order so fetch again
				update miplti set plti_stok = plti_stok - @pstok, plti_rqty = plti_rqty + @pstok
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
				if @@ROWCOUNT = 0 begin
					set @fail = 1
					set @lp = 0
					break
				end
			
				set @oqty = @pstok;
				set @sq = @sq - @pstok;
			end
			else begin     -- large plti to scr again
				update miplti set plti_stok = plti_stok - @sq, plti_rqty = plti_rqty + @sq
				where  plti_pltno = @pltno
				and    plti_lstk = @loca
				and    plti_prod = @matnr
				and    plti_loc  = @lgort
				and    plti_lot  = @charg
				and    plti_bestq  = '';
				if @@ROWCOUNT = 0 begin
					set @fail = 1
					set @lp = 0
					break
				end

				set @oqty = @sq;
				set @sq = 0; 
			end

			if @loca = null or  @pltno = null or  @oqty = null or @pksz = null begin
				set @fail = 1
				set @lp = 0
				break
			end

			if substring(@loca, 1, 1) = 'A' begin
				update milstk set lstk_io = '$', lstk_stat = '$R'  where lstk_no = @loca ;
			end
			
			INSERT INTO tiordx  
		 			 ( docnum,  sdno,  posnr,  lstk,   pltno,   qty,    flag,  pksz, credat,  cretim,   remark, idate,  itime,  oprod )  
		      VALUES ( @docnum, @sdno, @posnr, @loca,  @pltno,  @oqty, '$R',  @pksz, @odate,  @otime,  @remark, @idate, @itime, @oprod) ;
            if @@ROWCOUNT = 0 begin
				set @fail = 1
				set @lp = 0
				break;
			end
            set @toqty = @toqty + @oqty;		
				
			set @lp = @lp + 1;
		end

		if @fail = 1 break;  -- 실패

		if @lp > 0 begin
			update miordi set rqty = rqty + @toqty where docnum = @docnum and sdno = @sdno and posnr = @posnr;
			if @@ROWCOUNT = 0 begin
				set  @lp = 0;
				break;
			end	
		end
	end
	close c1;
	deallocate c1;

	RETURN @lp;
end
GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
--GRANT CONNECT TO [NT SERVICE\HealthService];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT CONNECT TO [NT Service\SqlServerExtension];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT CREATE DEFAULT TO PUBLIC;


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT CREATE PROCEDURE TO PUBLIC;


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT CREATE RULE TO PUBLIC;


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT CREATE TABLE TO PUBLIC;


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT CREATE VIEW TO PUBLIC;


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT DELETE
    ON OBJECT::[dbo].[pbcatcol] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT INSERT
    ON OBJECT::[dbo].[pbcatcol] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT SELECT
    ON OBJECT::[dbo].[pbcatcol] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT UPDATE
    ON OBJECT::[dbo].[pbcatcol] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT DELETE
    ON OBJECT::[dbo].[pbcatedt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT INSERT
    ON OBJECT::[dbo].[pbcatedt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT SELECT
    ON OBJECT::[dbo].[pbcatedt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT UPDATE
    ON OBJECT::[dbo].[pbcatedt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT DELETE
    ON OBJECT::[dbo].[pbcatfmt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT INSERT
    ON OBJECT::[dbo].[pbcatfmt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT SELECT
    ON OBJECT::[dbo].[pbcatfmt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT UPDATE
    ON OBJECT::[dbo].[pbcatfmt] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT DELETE
    ON OBJECT::[dbo].[pbcattbl] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT INSERT
    ON OBJECT::[dbo].[pbcattbl] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT SELECT
    ON OBJECT::[dbo].[pbcattbl] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT UPDATE
    ON OBJECT::[dbo].[pbcattbl] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT DELETE
    ON OBJECT::[dbo].[pbcatvld] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT INSERT
    ON OBJECT::[dbo].[pbcatvld] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT SELECT
    ON OBJECT::[dbo].[pbcatvld] TO PUBLIC
    AS [dbo];


GO
PRINT N'권한 권한을(를) 만드는 중...';


GO
GRANT UPDATE
    ON OBJECT::[dbo].[pbcatvld] TO PUBLIC
    AS [dbo];


GO
PRINT N'업데이트가 완료되었습니다.';


GO
