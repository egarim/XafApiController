using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo.Metadata.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace XafApiController.Module.Controllers
{
    //public class CriteriaKey : Attribute
    //{
    //    public string FieldNames { get; set; }
    //}
    //public  class ExcelImport 
    //{
       
    //    private bool Success;
       

    //    string[] GetObjectProperties(XPClassInfo classInfo, bool AddReferenceClassChildProperties, bool IncludeKeyField, ExcelImportMap CurrentExcelImportMap, ReadOnlyCollection<IRule> Rules)
    //    {
    //        if (classInfo != null)
    //            return GetObjectProperties(classInfo, new ArrayList(), AddReferenceClassChildProperties, IncludeKeyField, CurrentExcelImportMap, Rules);
    //        return new string[] { };
    //    }

    //    private static bool GetIsImporRequired(ReadOnlyCollection<IRule> Rules, XPMemberInfo m)
    //    {
    //        bool IsImporRequired;
    //        IEnumerable<IRule> rule = Rules.Where(r => r.UsedProperties.Contains(m.Name));
    //        if (!rule.Any())
    //        {
    //            IsImporRequired = false;
    //        }
    //        else
    //        {
    //            IsImporRequired = true;
    //        }
    //        return IsImporRequired;
    //    }
    //    private bool SkipMember(XPMemberInfo Member, bool IsReferencedMember)
    //    {
    //        HideOnImportMap Hide = null;
    //        if (IsReferencedMember)
    //        {
    //            Hide = (HideOnImportMap)Member.ReferenceType.FindAttributeInfo(typeof(HideOnImportMap));
    //            if (!ReferenceEquals(null, Hide))
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            Hide = (HideOnImportMap)Member.FindAttributeInfo(typeof(HideOnImportMap));
    //            if (!ReferenceEquals(null, Hide))
    //            {
    //                return true;
    //            }
    //            else
    //            {
    //                return false;
    //            }
    //        }

    //    }
    //    string[] GetObjectProperties(XPClassInfo xpoInfo, ArrayList processed, bool AddReferenceClassChildProperties, bool IncludeKeyField, ExcelImportMap CurrentExcelImportMap, ReadOnlyCollection<IRule> Rules)
    //    {
    //        var Os = this.View.ObjectSpace;
    //        CurrentExcelImportMap = Os.GetObject<ExcelImportMap>(CurrentExcelImportMap);
    //        if (processed.Contains(xpoInfo)) return new string[] { };
    //        processed.Add(xpoInfo);
    //        string FieldNames = string.Empty;
    //        ArrayList result = new ArrayList();
    //        bool IsReference;
    //        string caption;
    //        bool IsImporRequired;
    //        foreach (XPMemberInfo m in xpoInfo.PersistentProperties)
    //        {
    //            IsImporRequired = false;


    //            caption = CaptionHelper.GetMemberCaption(m.Owner.ClassType, m.Name);
    //            if (!(m is ServiceField) && m.IsPersistent)
    //            {

    //                CriteriaKey ImportAttribute;

    //                IsReference = (m.ReferenceType != null);

    //                if (SkipMember(m, IsReference))
    //                    continue;

    //                if ((IncludeKeyField == false) && (m.IsKey))
    //                    continue;


    //                result.Add(m.Name);
    //                if (IsReference)
    //                {
    //                    ImportAttribute = (CriteriaKey)m.ReferenceType.FindAttributeInfo(typeof(CriteriaKey));
    //                    if (!ReferenceEquals(null, ImportAttribute))
    //                    {
    //                        FieldNames = ImportAttribute.FieldNames;
    //                    }
    //                }
    //                else
    //                {
    //                    FieldNames = string.Empty;
    //                }

    //                IsImporRequired = GetIsImporRequired(Rules, m);
    //                CreateMapDetail(CurrentExcelImportMap, m.Name, FieldNames, Type.GetType(m.MemberType.FullName), IsReference, false, Os, m, caption, IsImporRequired);



    //                if (IsReference)
    //                {
    //                    if (AddReferenceClassChildProperties)
    //                    {
    //                        Rules = GetValidationRules(m.ReferenceType);
    //                        IsImporRequired = GetIsImporRequired(Rules, m);
    //                        string[] childProps = GetObjectProperties(m.ReferenceType, processed, AddReferenceClassChildProperties, IncludeKeyField, CurrentExcelImportMap, Rules);
    //                        foreach (string child in childProps)
    //                        {
    //                            string ChildProp = string.Format("{0}.{1}", m.Name, child);
    //                            result.Add(ChildProp);
    //                            CreateMapDetail(CurrentExcelImportMap, ChildProp, FieldNames, m.ReferenceType.ClassType, IsReference, false, Os, m, caption, IsImporRequired);
    //                        }
    //                    }
    //                }
    //            }
    //        }

    //        #region Import Collection NOT NEEDED
    //        if (CurrentExcelImportMap.IncludeCollections)
    //        {

    //            foreach (XPMemberInfo m in xpoInfo.CollectionProperties)
    //            {
    //                caption = CaptionHelper.GetMemberCaption(m.Owner.ClassType, m.Name);
    //                IsReference = true;
    //                Rules = GetValidationRules(m.ReferenceType);
    //                IsImporRequired = GetIsImporRequired(Rules, m);
    //                CriteriaKey ImportAttribute = (CriteriaKey)m.CollectionElementType.FindAttributeInfo(typeof(CriteriaKey));
    //                if (!ReferenceEquals(null, ImportAttribute))
    //                {
    //                    FieldNames = ImportAttribute.FieldNames;
    //                }
    //                else
    //                {
    //                    FieldNames = string.Empty;
    //                }

    //                if (CurrentExcelImportMap.IncludeCollectionChilds)
    //                {

    //                    string[] childProps = GetObjectProperties(m.CollectionElementType, processed, AddReferenceClassChildProperties, IncludeKeyField, CurrentExcelImportMap, Rules);
    //                    foreach (string child in childProps)
    //                    {
    //                        string ChildProp = string.Format("{0}.{1}", m.Name, child);
    //                        result.Add(ChildProp);
    //                        CreateMapDetail(CurrentExcelImportMap, ChildProp, FieldNames, m.CollectionElementType.ClassType, IsReference, true, Os, m, caption, IsImporRequired);
    //                    }
    //                }
    //                else
    //                {
    //                    //todo each prop is going to show as collection prop that is INCORRECT
    //                    CreateMapDetail(CurrentExcelImportMap, m.Name, FieldNames, m.CollectionElementType.ClassType, IsReference, true, Os, m, caption, IsImporRequired);
    //                }


    //            }
    //            #endregion
    //        }


    //        return result.ToArray(typeof(string)) as string[];
    //    }

    //    private static ReadOnlyCollection<IRule> GetValidationRules(XPClassInfo TypeInfo)
    //    {
    //        return Validator.RuleSet.GetRules(TypeInfo.ClassType, new ContextIdentifiers(STR_ImportContext));
    //    }
   
    //    private string GetKeyFieldNameCaptions(string KeyFields, XPClassInfo Owner, string field, XPMemberInfo member)
    //    {
    //        StringBuilder Builder = new StringBuilder();
    //        var Keys = KeyFields.Split(';');

    //        List<Tuple<String, Type>> KeysWithObjectType = new List<Tuple<String, Type>>();
    //        foreach (string key in Keys)
    //        {
    //            var BaseKey = key.Split('.');
    //            if (BaseKey.Length == 1)
    //            {

    //                //KeysWithObjectType.Add(new Tuple<String, Type>(BaseKey[0], Owner.ClassType));\
    //                KeysWithObjectType.Add(new Tuple<String, Type>(BaseKey[0], member.MemberType));
    //            }

    //            else
    //            {
    //                var OwnerMember = Owner.GetMember(field);
    //                XPMemberInfo OwnerMemberProperty = OwnerMember.ReferenceType.GetMember(BaseKey[0]);
    //                var othermember = OwnerMemberProperty.ReferenceType.ClassType;
    //                KeysWithObjectType.Add(new Tuple<String, Type>(BaseKey[1], othermember));
    //            }

    //        }
    //        foreach (var keyValuePairString in KeysWithObjectType)
    //        {
    //            Builder.Append(CaptionHelper.GetClassCaption(keyValuePairString.Item2.FullName));
    //            Builder.Append(CaptionHelper.GetClassCaption("."));
    //            Builder.Append(CaptionHelper.GetMemberCaption(keyValuePairString.Item2, keyValuePairString.Item1));
    //            Builder.Append("|");
    //        }
    //        return Builder.ToString().TrimEnd('|');

    //    }
       
    //    private object CastValue(object value, Type itemObjectType, int RowNumber)
    //    {
    //        object result = null;
    //        var converter = TypeDescriptor.GetConverter(itemObjectType);
    //        if (itemObjectType == typeof(DateTime))
    //        {
    //            result = DateTime.FromOADate(Convert.ToDouble(value)); ;
    //            return result;
    //        }

    //        //TODO remove posible double casting. We need to check the source first

    //        if (ReferenceEquals(null, value))
    //        {
    //            result = converter.ConvertFrom(value);
    //        }
    //        else
    //        {
    //            result = converter.ConvertFrom(value.ToString());
    //        }


    //        return result;
    //    }
    //    public static int ExcelColumnNameToNumber(string columnName)
    //    {
    //        if (string.IsNullOrEmpty(columnName)) throw new ArgumentNullException("columnName");

    //        columnName = columnName.ToUpperInvariant();

    //        int sum = 0;

    //        for (int i = 0; i < columnName.Length; i++)
    //        {
    //            sum *= 26;
    //            sum += (columnName[i] - 'A' + 1);
    //        }

    //        return sum;
    //    }
    //    [Obsolete("use ExcelColumnNameToNumber", true)]
    //    private int ConvertLetterToColumnNumber(string ColumnNumber)
    //    {
    //        byte[] asciiBytes = Encoding.ASCII.GetBytes(ColumnNumber);
    //        int asciiBytes1 = (int)(asciiBytes[0]);
    //        asciiBytes1 = asciiBytes1 - 64;
    //        return asciiBytes1;
    //    }
    //    private string ConvertNumbersToLetters(int Number)
    //    {
    //        int AsciiValue = Number + 64;
    //        if (AsciiValue < 91)
    //        {

    //            return Convert.ToChar(AsciiValue).ToString();
    //        }
    //        else if (AsciiValue > 90 && AsciiValue < 116)
    //        {
    //            AsciiValue = (Number - 26) + 64;
    //            return "A" + Convert.ToChar(AsciiValue).ToString();
    //        }
    //        throw new Exception("Excel column out of range");

    //    }
    //    protected void ParseExcelFile(string FileName, ExcelImportMap ExcelImportMap)
    //    {

    //        Excel.Application xlApp = new Excel.Application();
    //        Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(FileName, ReadOnly: true);
    //        Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Worksheets[ExcelImportMap.SheetName];
    //        Excel.Range xlRange = xlWorksheet.UsedRange;
    //        int CurrentRow = 0;
    //        string CurrentColumn = string.Empty;
    //        int rowCount = xlRange.Rows.Count;
    //        ContextIdentifiers ValidationContext = new ContextIdentifiers(STR_ImportContext);
    //        IObjectSpace objectSpace = this.Application.CreateObjectSpace();

    //        int INT_Row;
    //        if (ExcelImportMap.FirstRowIsHeader)
    //            INT_Row = ExcelImportMap.StartRow;
    //        else
    //            INT_Row = 1;

    //        try
    //        {
    //            if (ExcelImportMap.MaxImportRow > 0)
    //            {
    //                rowCount = ExcelImportMap.MaxImportRow;
    //            }


    //            for (int Row = INT_Row; Row <= rowCount; Row++)
    //            {



    //                CurrentRow = Row;
    //                XPCustomObject obj = (XPCustomObject)objectSpace.CreateObject(ExcelImportMap.TargetObjectType);
    //                foreach (var item in ExcelImportMap.ExcelImportMapDetails)
    //                {
    //                    CurrentColumn = item.SheetField;
    //                    if ((string.IsNullOrWhiteSpace(item.SheetField)) || (string.IsNullOrEmpty(item.SheetField)))
    //                        continue;

    //                    object value = null;
    //                    var ColumNumber = ExcelColumnNameToNumber(item.SheetField);
    //                    Excel.Range rng = null;

    //                    rng = xlWorksheet.Cells[Row, ColumNumber] as Excel.Range;

    //                    if (ReferenceEquals(rng.Value2, null))
    //                        continue;

    //                    if (!item.IsReferencesProperty)
    //                    {

    //                        value = rng.Value2;
    //                        object result = CastValue(value, item.ClassObjectType, Row);
    //                        //TODO check if value is string null or string empty
    //                        if (!(String.IsNullOrWhiteSpace(Convert.ToString(value)) || String.IsNullOrEmpty(Convert.ToString(value))))
    //                            obj.SetMemberValue(item.ObjectField, result);
    //                    }
    //                    if (item.IsReferencesProperty)
    //                    {
    //                        List<CriteriaOperator> operators = new List<CriteriaOperator>();
    //                        string[] ImportRowValues = rng.Value2.ToString().Split('|');
    //                        //foreach (string ImportRowValue in ImportRowValues)
    //                        //{
    //                        //    ImportRowValue.Split(';');
    //                        //}
    //                        //var values = rng.Value2.ToString().Split(';');

    //                        //var fields = item.KeyFields.Split(';');
    //                        var fields = item.KeyFields2.Split(';');
    //                        for (int j = 0; j < ImportRowValues.Length; j++)
    //                        {

    //                            operators.Add(new BinaryOperator(fields[j], CastValue(ImportRowValues[j], typeof(string), Row)));
    //                        }

    //                        var op = BinaryOperator.And(operators);
    //                        value = objectSpace.FindObject(item.ClassObjectType, op);
    //                        obj.SetMemberValue(item.ObjectField, value);
    //                    }
    //                    //if (!item.IsCollection)
    //                    //{
    //                    //}



    //                }
    //                //TODO FIX13
    //                //Validator.RuleSet.Validate(obj, ValidationContext);
    //            }

    //            if (objectSpace.IsModified)
    //            {
    //                objectSpace.CommitChanges();
    //                Success = true;
    //            }





    //        }
    //        catch (Exception Ex)
    //        {
    //            throw new UserFriendlyException(String.Format("Error en importando la fila {0}, en la columna {2} |{1}", CurrentRow, Ex.Message, CurrentColumn));
    //        }
    //        finally
    //        {
    //            xlWorkbook.Close();
    //            xlWorksheet = null;
    //            xlWorkbook = null;
    //            xlApp = null;
    //        }
    //        if (Success)
    //        {
    //            throw new UserFriendlyException(string.Format("Se importaron {0} registros", (CurrentRow - 1)));
    //        }

    //    }

     
    //}
}
