using System;
using System.Collections;

namespace CarDealer
{

    public enum JobStatusType
    {
        Open = 0,
        Close = 1
    }

    public enum PageStateType
    {
        Init = 0,
        Postback = 1,
        Done = 2
    }

    public enum PointerClassType
    {
        None = 0,
        Configuration = 1,
        Message = 2
    }

    public struct FeedFormatMimeType
    {
        public const String Rss20 = "application/rss+xml";
        public const String Atom10 = "application/atom+xml";
    }

    // 
    public enum EnvironmentType
    {
        Windows = 1,
        Web = 2,
        WebService = 3,
        WindowsService = 4,
        ClassLibrary = 5
    }

    public enum ObjectActionType
    {
        None = 1,
        View = 2,
        Update = 3,
        Create = 4,
        Delete = 5
    }

    public enum DotNetDataType
    {
        Boolean = 1,
        Byte = 2,
        Char = 3,
        Date = 4,
        Decimal = 5,
        Double = 6,
        Integer = 7,
        Long = 8,
        Object = 9,
        SByte = 10,
        Short = 11,
        Single = 12,
        String = 13,
        UInteger = 14,
        ULong = 15,
        Struct = 16,
        UShort = 17
    }

    public enum SQLServerDataType
    {
        Boolean = 1,
        Byte = 2,
        Char = 3,
        DateTime = 4,
        Decimal = 5,
        Double = 6,
        Int16 = 7,
        Int32 = 8,
        Int64 = 9,
        SByte = 10,
        Single = 11,
        String = 12,
        TimeSpan = 13,
        UInt16 = 14,
        UInt32 = 15,
        UInt64
    }

    public enum SQLCommand
    {
        select = 1,
        insert = 2,
        update = 3,
        delete = 4,
        count = 5,
        truncate = 6,
        drop = 7,
        distinct = 8
    }
    
    public enum SQLQueryCommand
    {
        GetSelectCommand = 1,
        GetInsertCommand = 2,
        GetUpdateCommand = 3,
        GetDeleteCommand = 4
    }

    public enum ColumnRequiredType
    {
        NullAllowed = 1,
        NullNotAllowed = 2
    }

    public enum ErrorNotificationMethodType
    {
        Ignore = 0,
        File = 1,
        Database = 2,
        Email = 3,
        Screen = 4,
        EventLog = 5
    }

    public enum RoleType
    {
        Anonymous = 1,
        User = 2,
        Admin = 3,
        SuperAdmin = 4,
        Client = 5
    }


    public enum ClientSecurityType
    {
        Guest = 0,
        Page = 1,
        Module = 2,
        Role = 3
    }

    public enum ExceptionType
    {
        None = 1,
        ADSI = 2,
        Communication = 3,
        Data = 4,
        Error = 5,
        Graphics = 6,
        Security = 7,
        Storage = 8,
        Web = 9,
        XML = 10,
        Custom = 11
    }

    public enum WebPageStateType
    {
        Initialize = 1,
        Edit = 2,
        Save = 3,
        View = 4,
        List = 5,
        Done = 6,
        Finish = 7,
        Back = 8,
        Exit = 9,
        Complete = 10
    }


    // declare all application constants below
    public class Constants
    {

        public Constants()
        {
        }

        public static readonly string TABLE_PERSON = "Person";
        public static readonly string TABLE_CATEGORY = "Category";
        public static readonly string TABLE_CITY = "CITY";
        public static readonly string TABLE_COMPANY = "Company";
        public static readonly string TABLE_CONTACT = "Contact";
        public static readonly string TABLE_LOGIN = "Login";
        public static readonly string TABLE_STATE = "STATE";

        public static readonly string GENERATED_PASSWORD = "guest";
        public static readonly string NONE = "NONE";

        // Application Configuration Keys
        public static readonly string APP_SETTING_Server_Type = "ServerType";

        // Calendar Daily constants
        public static readonly string VIEW_PREFIX = "v";

        // string manipulation constants
        public static readonly string QUESTION_MARK = "?";
        public static readonly string AMPERSAND = "&";
        public static readonly string FORWARD_SLASH = "/";
        public static readonly string DASH = "-";
        public static readonly string UNDER_SCORE = "_";

        // Link open constants        
        public static readonly string ANCHOR_BLANK = "_blank";
        public static readonly string ANCHOR_PARENT = "_parent";
        public static readonly string ANCHOR_SELF = "_self";
        public static readonly string ANCHOR_TOP = "_top";

        // database SQL
        public static readonly string SQL_ALL = "*";
        public static readonly string SQL_NOT_USED = "NOT_USED";
        public static readonly string PRIMARY_KEY = "PK";
        public static readonly string COMPOSITE_KEY = "CompositeKey";
        public static readonly string FOREIGN_KEY = "FK";

        // default field sizes
        public static readonly int Code = 50;
        public static readonly int LongCode = 75;

        public static readonly int Description = 100;
        public static readonly int LongDescription = 200;

        public static readonly int Comment = 250;
        public static readonly int LongComment = 350;

        public static readonly int Line = 500;
        public static readonly int LongLine = 1000;

        public static readonly int Buffer = 5000;
        public static readonly int LongBuffer = 10000;

        public static readonly string NEWLINE = "\r\n";
        public static readonly string NEWLINE_FILE = "\r\n";
        public static readonly string NEWLINE_WEB = "<br />";

        public static readonly Char EDI_DELIMETER = '*';
        
        // Characters using for string split and pack
        public static readonly Char CharComma = ','; 

        public const string DEFAULT_COUNTRY_NAME = "United States of America";
        public const string DEFAULT_COUNTRY_CODE = "US";

        public const string SUB = "+--- ";
        public const string SPACE = " ";
        public const string COMMA = ",";
        public const string COLON = ":";

        public const string SELECTED_DATE = "mm/dd/yyyy";
        public const string SELECTED_TIME = "- Select -";
        public const string NO_DATE = "1/1/1901";
        public const string GUID_NO_FILE = "FDADEF9C-D59E-41D2-ACD1-A4D025044FD3";

        // constants for SMTP
        public const string GMAIL = "GMAIL";

        public static readonly string BorderCollapse = "border-collapse";
        public static readonly string CantCreateTable = "Unable to create the table.";
        public static readonly string FileIsReadOnly = "File is read only";
        public static readonly string JSConfirm = "javascript:if(confirm('{0}') == false) return false;";
        public static readonly string Modified = "Last Modified";

    }
}
