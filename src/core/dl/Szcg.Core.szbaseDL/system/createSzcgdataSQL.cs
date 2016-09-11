using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace bacgDL.system
{
   public  class createSzcgdataSQL
    {
       public static string getSZCG_bak_sql1(string spath,string p_year)
       {
           return string.Format(@"
                CREATE DATABASE [szcg_bak{1}] ON  PRIMARY 
                ( NAME = N'szcg_bak{1}', FILENAME = N'{0}\szcg_bak{1}.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
                 LOG ON 
                ( NAME = N'szcg_bak{1}_log', FILENAME = N'{0}\szcg_bak{1}_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
                ;
                ALTER DATABASE [szcg_bak{1}] SET COMPATIBILITY_LEVEL = 100
                ;", spath, p_year);
       }

       
       //数据库建表语句
          public static string getSZCG_bak_sql2(string p_year)
{
    string sql = string.Format(@"
CREATE TABLE {0}[dbo].[b_web_proj](
	[id] [int] NOT NULL,
	[WebID] [varchar](50) NULL,
	[IsExchanged] [bit] NULL,
	[projcode] [int] NULL,
	[cudate] [datetime] NULL,
	[Name] [varchar](32) NULL,
	[tel] [varchar](32) NULL,
	[ip] [varchar](32) NULL,
	[type] [varchar](10) NULL,
	[email] [varchar](50) NULL,
	[contacts] [varchar](150) NULL,
	[typecode] [bit] NULL,
	[bigclass] [char](4) NULL,
	[smallclass] [char](4) NULL,
	[address] [varchar](300) NULL,
	[title] [varchar](50) NULL,
	[probdesc] [varchar](1024) NULL,
	[IsFeedBack] [bit] NULL,
	[FeedbackContent] [varchar](500) NULL,
	[FeedbackDate] [datetime] NULL,
	[memo] [varchar](500) NULL,
	[imagesource] [varchar](200) NULL,
	[imageresult] [varchar](200) NULL,
	[streetcode] [int] NULL,
	[commcode] [varchar](12) NULL,
	[fid] [varchar](64) NULL,
	[verifycode] [varchar](12) NULL,
 CONSTRAINT [PK_B_WEB_PROJ] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;

CREATE TABLE {0}[dbo].[b_public_proj](
	[id] [int] NOT NULL,
	[acceptcode] [varchar](20) NULL,
	[IsExchanged] [bit] NULL,
	[projcode] [int] NULL,
	[usercode] [varchar](32) NULL,
	[username] [varchar](18) NULL,
	[SeatTel] [varchar](20) NULL,
	[seatIP] [varchar](20) NULL,
	[ccid] [varchar](20) NULL,
	[ceid] [varchar](20) NULL,
	[Name] [varchar](32) NULL,
	[tel] [varchar](32) NULL,
	[cudate] [datetime] NULL,
	[filename] [varchar](128) NULL,
	[desaddress] [varchar](128) NULL,
	[Srcaddress] [varchar](128) NULL,
	[isret] [char](1) NULL,
	[groupid] [varchar](32) NULL,
	[retcount] [int] NULL,
	[type] [int] NULL,
	[retdate] [datetime] NULL,
	[loginname] [varchar](18) NULL,
	[acceptareacode] [varchar](18) NULL,
	[memo] [varchar](1024) NULL,
	[WaitSecond] [int] NULL,
	[CallLength] [int] NULL,
	[VoiceType] [char](1) NULL,
 CONSTRAINT [PK_B_PUBLIC_PROJ] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project_trace_time]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
CREATE TABLE {0}[dbo].[b_project_trace_time](
	[projcode] [int] NOT NULL,
	[Time_2_00] [datetime] NULL,
	[Time_2_01] [datetime] NULL,
	[Time_2_02] [datetime] NULL,
	[Time_2_03] [datetime] NULL,
	[Time_3_01] [datetime] NULL,
	[Time_3_02] [datetime] NULL,
	[Time_3_03] [datetime] NULL,
	[Time_3_04] [datetime] NULL,
	[Time_6_01] [datetime] NULL,
	[Time_6_03] [datetime] NULL,
	[Time_6_07] [datetime] NULL,
	[Time_6_09] [datetime] NULL,
	[Time_7_00] [datetime] NULL,
	[Time_7_09] [datetime] NULL,
	[Time_8_00] [datetime] NULL,
	[Time_8_01] [datetime] NULL,
	[Time_8_02] [datetime] NULL,
	[Time_8_03] [datetime] NULL,
	[Time_8_04] [datetime] NULL,
	[Time_8_05] [datetime] NULL,
	[Time_8_09] [datetime] NULL,
	[Time_9_00] [datetime] NULL,
	[Time_9_09] [datetime] NULL,
	[Time_10_01] [datetime] NULL,
	[Time_101_01] [datetime] NULL,
	[Time_102_01] [datetime] NULL,
	[Time_102_02] [datetime] NULL,
	[Time_11_00] [datetime] NULL,
	[Time_11_09] [datetime] NULL,
	[Time_13_03] [datetime] NULL,
	[Time_14_01] [datetime] NULL,
	[Time_14_02] [datetime] NULL,
	[Time_15_01] [datetime] NULL,
	[Time_15_02] [datetime] NULL,
	[Num_2_00] [int] NULL,
	[Num_2_01] [int] NULL,
	[Num_2_02] [int] NULL,
	[Num_2_03] [int] NULL,
	[Num_3_01] [int] NULL,
	[Num_3_02] [int] NULL,
	[Num_3_03] [int] NULL,
	[Num_3_04] [int] NULL,
	[Num_6_01] [int] NULL,
	[Num_6_03] [int] NULL,
	[Num_6_07] [int] NULL,
	[Num_6_09] [int] NULL,
	[Num_7_00] [int] NULL,
	[Num_7_09] [int] NULL,
	[Num_8_00] [int] NULL,
	[Num_8_01] [int] NULL,
	[Num_8_02] [int] NULL,
	[Num_8_03] [int] NULL,
	[Num_8_04] [int] NULL,
	[Num_8_05] [int] NULL,
	[Num_8_09] [int] NULL,
	[Num_9_00] [int] NULL,
	[Num_9_09] [int] NULL,
	[Num_10_01] [int] NULL,
	[Num_101_01] [int] NULL,
	[Num_102_01] [int] NULL,
	[Num_102_02] [int] NULL,
	[Num_11_00] [int] NULL,
	[Num_11_09] [int] NULL,
	[Num_13_03] [int] NULL,
	[Num_14_01] [int] NULL,
	[Num_14_02] [int] NULL,
	[Num_15_01] [int] NULL,
	[Num_15_02] [int] NULL
) ON [PRIMARY]
;
/****** Object:  Table [dbo].[b_project_trace]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project_trace](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[stepid] [smallint] NULL,
	[stateid] [smallint] NULL,
	[ButtonCode] [varchar](20) NULL,
	[PreNodeId] [varchar](5) NULL,
	[CurrentNodeId] [varchar](5) NULL,
	[CurrentBusiStatus] [varchar](5) NULL,
	[actionname] [varchar](18) NULL,
	[cu_date] [datetime] NULL,
	[usercode] [int] NULL,
	[DepartCode] [varchar](20) NULL,
	[roleid] [varchar](20) NULL,
	[_opinion] [varchar](1024) NULL,
	[returntracetag] [bit] NULL,
	[memo] [varchar](200) NULL,
	[ChangeStatus] [nchar](1) NULL,
	[ChangeDate] [datetime] NULL,
	[istimeout] [int] NULL,
 CONSTRAINT [PK_B_PROJECT_TRACE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project_sound]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project_sound](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[soundstate] [char](1) NULL,
	[soundpath] [varchar](255) NULL,
	[cudate] [datetime] NULL,
	[memo] [varchar](512) NULL,
 CONSTRAINT [PK_B_PROJECT_SOUND] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project_score]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project_score](
	[projcode] [int] NOT NULL,
	[collcode] [nvarchar](50) NULL,
	[departcode] [nvarchar](50) NULL,
	[gridcode] [varchar](18) NULL,
	[findScore] [numeric](9, 2) NULL,
	[disposalScore] [numeric](9, 2) NULL,
	[memo] [varchar](150) NULL,
	[operateTime] [datetime] NULL,
	[isAssured] [bit] NULL
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project_inspect]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project_inspect](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[usercode] [int] NULL,
	[roleid] [varchar](9) NULL,
	[content] [varchar](512) NULL,
	[Leader] [varchar](50) NULL,
	[cudate] [datetime] NULL,
	[ChangeStatus] [char](1) NULL,
	[ChangeDate] [datetime] NULL,
 CONSTRAINT [PK_B_PROJECT_INSPECT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project_file]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project_file](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[filestate] [char](1) NULL,
	[filepath] [varchar](255) NULL,
	[cudate] [datetime] NULL,
	[memo] [varchar](512) NULL,
 CONSTRAINT [PK_B_PROJECT_FILE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project_feedback]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project_feedback](
	[projcode] [int] NOT NULL,
	[FeedbackDate] [datetime] NULL,
	[Feedbacker] [int] NULL,
	[FeedbackMode] [smallint] NULL,
	[FeedbackTarget] [varchar](50) NULL,
	[FeedbackTargetMobile] [varchar](200) NULL,
	[FeedbackContent] [varchar](500) NULL
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project_detail]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project_detail](
	[projcode] [int] NOT NULL,
	[projname] [varchar](30) NULL,
	[area] [varchar](18) NULL,
	[street] [varchar](100) NULL,
	[square] [varchar](100) NULL,
	[probdesc] [varchar](1024) NULL,
	[VerifyDesc] [varchar](1024) NULL,
	[Tracer] [varchar](18) NULL,
	[Memo] [varchar](512) NULL,
	[typename] [varchar](10) NULL,
	[bigclassname] [varchar](20) NULL,
	[smallclassname] [varchar](50) NULL,
 CONSTRAINT [PK_B_PROJECT_DETAIL] PRIMARY KEY CLUSTERED 
(
	[projcode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_project]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_project](
	[projcode] [int] NOT NULL,
	[stepid] [smallint] NULL,
	[stateid] [smallint] NULL,
	[ButtonCode] [varchar](20) NULL,
	[NodeId] [varchar](5) NULL,
	[role] [int] NULL,
	[StepDate] [datetime] NULL,
	[TargetDepartCode] [varchar](20) NULL,
	[DoDepartCode] [varchar](20) NULL,
	[DoRole] [int] NULL,
	[startdate] [datetime] NULL,
	[enddate] [datetime] NULL,
	[Telephonist] [varchar](18) NULL,
	[TelephonistCode] [int] NULL,
	[IsManual] [bit] NULL,
	[probsource] [varchar](6) NULL,
	[typecode] [int] NULL,
	[bigclass] [char](4) NULL,
	[smallclass] [char](4) NULL,
	[detailedclass] [char](8) NULL,
	[AreaID] [varchar](18) NULL,
	[StreetID] [varchar](18) NULL,
	[SquareID] [varchar](18) NULL,
	[gridcode] [varchar](18) NULL,
	[address] [varchar](200) NULL,
	[fid] [varchar](64) NULL,
	[PartID] [varchar](64) NULL,
	[release] [char](1) NULL,
	[isdel] [char](1) NULL,
	[istransaction] [bit] NULL,
	[locktime] [datetime] NULL,
	[lockusercode] [int] NULL,
	[withDept] [char](1) NULL,
	[isgreat] [char](1) NULL,
	[ispress] [bit] NULL,
	[istimeout] [bit] NULL,
	[isthrough] [char](1) NULL,
	[groupid] [varchar](10) NULL,
	[isProcess] [bit] NULL,
	[ProcessType] [smallint] NULL,
	[IsNeedFeedBack] [bit] NULL,
	[IsFeedBack] [bit] NULL,
	[FeedbackDate] [datetime] NULL,
	[Feedbacker] [int] NULL,
	[FeedbackMode] [smallint] NULL,
	[RequestProcessType] [smallint] NULL,
	[ChangeStatus] [char](1) NULL,
	[ChangeDate] [datetime] NULL,
	[WorkGrid] [varchar](18) NULL,
	[IsReturned] [bit] NULL,
	[NoPassNum] [tinyint] NULL,
	[isManyDept] [varchar](100) NULL,
	[DeptProjectState] [nchar](10) NULL,
	[departTime] [int] NULL,
	[departTimeType] [smallint] NULL,
	[traceTime] [datetime] NULL,
    [ispress_jc] [varchar](10) NULL,
	[isask_jc] [varchar](10) NULL,
	[oldId] [int] NULL,
 CONSTRAINT [PK_b_project] PRIMARY KEY CLUSTERED 
(
	[projcode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;

/****** Object:  Table [dbo].[b_pdamsg_Trace]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_pdamsg_Trace](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[collcode] [int] NULL,
	[cu_date] [datetime] NULL,
	[ioflag] [char](1) NULL,
	[state] [char](1) NULL,
	[IMSI] [varchar](20) NULL,
	[IMEI] [varchar](20) NULL,
 CONSTRAINT [PK_B_PDAMSG_TRACE] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_pdamsg]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_pdamsg](
	[id] [int] NOT NULL,
	[projcode] [int] NOT NULL,
	[collcode] [int] NULL,
	[cu_date] [datetime] NULL,
	[msgcontent] [varchar](1024) NULL,
	[msgtitle] [varchar](128) NULL,
	[ioflag] [char](1) NULL,
	[state] [char](1) NULL,
	[memo] [varchar](512) NULL,
	[resend] [int] NULL,
	[re_cu_date] [datetime] NULL,
	[ChangeStatus] [char](1) NULL,
	[ChangeDate] [datetime] NULL,
	[collcode_up] [int] NULL,
 CONSTRAINT [PK_B_PDAMSG] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_other_proj]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_other_proj](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[name] [varchar](64) NULL,
	[tel] [varchar](32) NULL,
	[type] [varchar](32) NULL,
	[isret] [char](1) NULL,
	[cudate] [datetime] NULL,
	[retdate] [datetime] NULL,
	[retcount] [int] NULL,
	[memo] [varchar](1024) NULL,
	[accept] [int] NULL,
	[ip] [varchar](32) NULL,
 CONSTRAINT [PK_B_OTHER_PROJ] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_opinion_feedback]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_opinion_feedback](
	[id] [int] NOT NULL,
	[usercode] [int] NULL,
	[fid] [int] NULL,
	[cudate] [datetime] NULL,
	[content] [varchar](1024) NULL,
	[memo] [varchar](1024) NULL,
 CONSTRAINT [PK_B_OPINION_FEEDBACK] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_opinion_collect]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_opinion_collect](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[title] [varchar](128) NULL,
	[cudate] [datetime] NULL,
	[content] [varchar](1024) NULL,
	[usercode] [int] NULL,
	[memo] [varchar](1024) NULL,
 CONSTRAINT [PK_B_OPINION_COLLECT] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;
/****** Object:  Table [dbo].[b_lawer_proj]    Script Date: 03/26/2012 15:24:15 ******/
SET ANSI_NULLS ON
;
SET QUOTED_IDENTIFIER ON
;
SET ANSI_PADDING ON
;
CREATE TABLE {0}[dbo].[b_lawer_proj](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[lawercode] [int] NULL,
	[name] [varchar](32) NULL,
	[tel] [varchar](32) NULL,
	[type] [varchar](32) NULL,
	[isret] [char](1) NULL,
	[cu_date] [datetime] NULL,
	[ip] [varchar](32) NULL,
 CONSTRAINT [PK_B_LAWER_PROJ] PRIMARY KEY NONCLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
SET ANSI_PADDING OFF
;

CREATE TABLE {0}[dbo].[b_project_ask_jc](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[usercode] [int] NULL,
	[roleid] [varchar](9) NULL,
	[fromdepartcode] [varchar](9) NULL,
	[todepartcode] [varchar](9) NULL,
	[content] [varchar](512) NULL,
	[ifweituo] [int] NULL,
	[cudate] [datetime] NULL,
	[ChangeStatus] [char](1) NULL,
	[ChangeDate] [datetime] NULL,
	[repeatcontent] [varchar](2048) NULL,
	[repeatperson] [varchar](20) NULL,
	[repeatstatus] [varchar](9) NULL,
	[repeattime] [datetime] NULL,
	[ifagreen] [int] NULL,
 CONSTRAINT [PK_B_PROJECT_ask_jc] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE {0}[dbo].[b_project_inspect_cx](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[usercode] [int] NULL,
	[roleid] [varchar](9) NULL,
	[content] [varchar](512) NULL,
	[Leader] [varchar](50) NULL,
	[cudate] [datetime] NULL,
	[ChangeStatus] [char](1) NULL,
	[ChangeDate] [datetime] NULL,
	[repeatcontent] [varchar](2048) NULL,
	[repeatperson] [varchar](20) NULL,
	[inspectype] [varchar](9) NULL,
	[repeatstatus] [varchar](9) NULL,
	[repeattime] [datetime] NULL,
 CONSTRAINT [PK_B_PROJECT_INSPECT_CX] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
CREATE TABLE {0}[dbo].[b_project_inspect_jc](
	[id] [int] NOT NULL,
	[projcode] [int] NULL,
	[usercode] [int] NULL,
	[roleid] [varchar](9) NULL,
	[fromdepartcode] [varchar](9) NULL,
	[todepartcode] [varchar](9) NULL,
	[content] [varchar](512) NULL,
	[ifweituo] [int] NULL,
	[cudate] [datetime] NULL,
	[ChangeStatus] [char](1) NULL,
	[ChangeDate] [datetime] NULL,
	[repeatcontent] [varchar](2048) NULL,
	[repeatperson] [varchar](20) NULL,
	[repeatstatus] [varchar](9) NULL,
	[repeattime] [datetime] NULL,
	[ifagreen] [int] NULL,
 CONSTRAINT [PK_B_PROJECT_INSPECT_jc] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
;
/****** Object:  Default [DF__b_project__Num_2__5D80D6A1]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_2__5D80D6A1]  DEFAULT ((0)) FOR [Num_2_00]
;
/****** Object:  Default [DF__b_project__Num_2__5E74FADA]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_2__5E74FADA]  DEFAULT ((0)) FOR [Num_2_01]
;
/****** Object:  Default [DF__b_project__Num_2__5F691F13]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_2__5F691F13]  DEFAULT ((0)) FOR [Num_2_02]
;
/****** Object:  Default [DF__b_project__Num_2__605D434C]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_2__605D434C]  DEFAULT ((0)) FOR [Num_2_03]
;
/****** Object:  Default [DF__b_project__Num_3__61516785]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_3__61516785]  DEFAULT ((0)) FOR [Num_3_01]
;
/****** Object:  Default [DF__b_project__Num_3__62458BBE]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_3__62458BBE]  DEFAULT ((0)) FOR [Num_3_02]
;
/****** Object:  Default [DF__b_project__Num_3__6339AFF7]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_3__6339AFF7]  DEFAULT ((0)) FOR [Num_3_03]
;
/****** Object:  Default [DF__b_project__Num_3__642DD430]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_3__642DD430]  DEFAULT ((0)) FOR [Num_3_04]
;
/****** Object:  Default [DF__b_project__Num_6__6521F869]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_6__6521F869]  DEFAULT ((0)) FOR [Num_6_01]
;
/****** Object:  Default [DF__b_project__Num_6__66161CA2]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_6__66161CA2]  DEFAULT ((0)) FOR [Num_6_03]
;
/****** Object:  Default [DF__b_project__Num_6__670A40DB]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_6__670A40DB]  DEFAULT ((0)) FOR [Num_6_07]
;
/****** Object:  Default [DF__b_project__Num_6__67FE6514]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_6__67FE6514]  DEFAULT ((0)) FOR [Num_6_09]
;
/****** Object:  Default [DF__b_project__Num_7__68F2894D]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_7__68F2894D]  DEFAULT ((0)) FOR [Num_7_00]
;
/****** Object:  Default [DF__b_project__Num_7__69E6AD86]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_7__69E6AD86]  DEFAULT ((0)) FOR [Num_7_09]
;
/****** Object:  Default [DF__b_project__Num_8__6ADAD1BF]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_8__6ADAD1BF]  DEFAULT ((0)) FOR [Num_8_00]
;
/****** Object:  Default [DF__b_project__Num_8__6BCEF5F8]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_8__6BCEF5F8]  DEFAULT ((0)) FOR [Num_8_01]
;
/****** Object:  Default [DF__b_project__Num_8__6CC31A31]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_8__6CC31A31]  DEFAULT ((0)) FOR [Num_8_02]
;
/****** Object:  Default [DF__b_project__Num_8__6DB73E6A]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_8__6DB73E6A]  DEFAULT ((0)) FOR [Num_8_03]
;
/****** Object:  Default [DF__b_project__Num_8__6EAB62A3]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_8__6EAB62A3]  DEFAULT ((0)) FOR [Num_8_09]
;
/****** Object:  Default [DF__b_project__Num_9__6F9F86DC]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_9__6F9F86DC]  DEFAULT ((0)) FOR [Num_9_00]
;
/****** Object:  Default [DF__b_project__Num_9__7093AB15]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_9__7093AB15]  DEFAULT ((0)) FOR [Num_9_09]
;
/****** Object:  Default [DF__b_project__Num_1__7187CF4E]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_1__7187CF4E]  DEFAULT ((0)) FOR [Num_10_01]
;
/****** Object:  Default [DF__b_project__Num_1__727BF387]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_1__727BF387]  DEFAULT ((0)) FOR [Num_101_01]
;
/****** Object:  Default [DF__b_project__Num_1__737017C0]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_1__737017C0]  DEFAULT ((0)) FOR [Num_102_01]
;
/****** Object:  Default [DF__b_project__Num_1__74643BF9]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_1__74643BF9]  DEFAULT ((0)) FOR [Num_102_02]
;
/****** Object:  Default [DF__b_project__Num_1__75586032]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace_time] ADD  CONSTRAINT [DF__b_project__Num_1__75586032]  DEFAULT ((0)) FOR [Num_11_00]
;
/****** Object:  Default [DF_b_project_trace_ChangeStatus]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace] ADD  CONSTRAINT [DF_b_project_trace_ChangeStatus]  DEFAULT ((0)) FOR [ChangeStatus]
;
/****** Object:  Default [DF__b_project__istim__1ED998B2]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project_trace] ADD  DEFAULT ((0)) FOR [istimeout]
;
/****** Object:  Default [DF_b_project_ChangeStatus]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project] ADD  CONSTRAINT [DF_b_project_ChangeStatus]  DEFAULT ((0)) FOR [ChangeStatus]
;
/****** Object:  Default [DF__b_project__depar__1BFD2C07]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project] ADD  CONSTRAINT [DF__b_project__depar__1BFD2C07]  DEFAULT ((0)) FOR [departTime]
;
/****** Object:  Default [DF__b_project__depar__1CF15040]    Script Date: 03/26/2012 15:24:15 ******/
ALTER TABLE {0}[dbo].[b_project] ADD  CONSTRAINT [DF__b_project__depar__1CF15040]  DEFAULT ((0)) FOR [departTimeType]
;

", "szcg_bak" + p_year + ".");

        return sql;
}
    }
}
