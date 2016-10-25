using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
//using VCity.Core;
//using UnityEngine;
//using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
using UnityEditor;
/// <summary>
/// Utility class for creating scriptable object assets.
/// </summary>
public static class CustomAssetUtility
{
    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (File.Exists(path)) // modified this line, folders can have extensions.
        {
            path = path.Replace("/" + Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }      

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).Name + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
#endif

public class NormalSingletion<T> where T : new()
{
    protected static T s_pInstance = new T();
    public static T Instance
    {
        get
        {
            return s_pInstance;
        }
    }
}

namespace FairyGUI.UI
{
    //public class UtilsCommon : NormalSingletion<UtilsCommon>
    //{
    //    System.Drawing.Font _fontDefault;// = new System.Drawing.Font("宋体", 20);
    //    protected System.Drawing.Font FontDefault
    //    {
    //        private set { }
    //        get
    //        {
    //            if (null == _fontDefault)
    //            {
    //                _fontDefault = new System.Drawing.Font("宋体", 20);
    //            }
    //            return _fontDefault;
    //        }
    //    }
    //    public Bitmap TextToBitmapAllDefault(string text)
    //    {
    //        return UtilsCommonS.TextToBitmap(text, FontDefault, Rectangle.Empty, System.Drawing.Color.Black, System.Drawing.Color.White);
    //    }
    //}

    public static class UtilsCommonS
    {
        public static StringBuilder StrBuilder = new StringBuilder();

        #region String
        public static byte[] String2ByteDefault(string str)
        {
            return Encoding.Default.GetBytes(str);
        }

        public static string Byte2StringDefault(byte[] byt)
        {
            return Encoding.Default.GetString(byt);
        }

        public static byte[] String2ByteUTF8(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string Byte2StringUTF8(byte[] byt)
        {
            if (null == byt)
            {
                return string.Empty;
            }
            return Encoding.UTF8.GetString(byt);
        }
        #endregion

        public static string Time2String(string formart = "yyyy-MM-dd_HH.mm.ss")
        {
            return DateTime.Now.ToString(formart);
        }

        public static string GetStringSourceOrEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            else
            {
                return str;
            }
        }

        public static bool IsStringNullOrEmptyOrDefault(string str, string strDefault = "null")
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            else
            {
                return str == strDefault;
            }
        }

        /// <summary>
        /// 之后可以和其他方法测试效率
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsIntNumber(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Math
        public static int FloatCeilingToInt(float f)
        {
            return (int)Math.Ceiling((double)f);
        }
        #endregion Math

        //#region Image

        ///// <summary>
        ///// 把文字转换才Bitmap
        ///// new System.Drawing.Font("宋体", 12)
        ///// Rectangle.Empty
        ///// Brushes.Black
        ///// System.Drawing.Color.White
        ///// </summary>
        ///// <param name="text"></param>
        ///// <param name="font"></param>
        ///// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        ///// <param name="fontcolor">字体颜色</param>
        ///// <param name="backColor">背景颜色</param>
        ///// <returns></returns>
        //public static Bitmap TextToBitmap(string text, System.Drawing.Font font, Rectangle rect, System.Drawing.Color fontColor,
        //    System.Drawing.Color backColor)
        //{
        //    if (string.IsNullOrEmpty(text))
        //    {
        //        return null;
        //    }
        //    System.Drawing.Graphics g;
        //    Bitmap bmp;
        //    StringFormat format = new StringFormat(StringFormatFlags.NoClip);
        //    //new PointF() 
        //    if (rect == Rectangle.Empty)
        //    {
        //        bmp = new Bitmap(1, 1);
        //        g = System.Drawing.Graphics.FromImage(bmp);
        //        var Sz = TextRenderer.MeasureText(g, text, font);
        //        bmp.Dispose();
        //        bmp = new Bitmap(Sz.Width, Sz.Height);
        //        rect = new Rectangle(0, 0, Sz.Width, Sz.Height);

        //        //bmp = new Bitmap(1, 1);
        //        //g = System.Drawing.Graphics.FromImage(bmp);
        //        ////计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
        //        //SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

        //        //int width = (int)(sizef.Width + 1);
        //        //int height = (int)(sizef.Height + 1);
        //        //rect = new Rectangle(0, 0, width, height);
        //        //bmp.Dispose();
        //        //bmp = new Bitmap(width, height);
        //    }
        //    else
        //    {
        //        bmp = new Bitmap(rect.Width, rect.Height);
        //    }

        //    g = System.Drawing.Graphics.FromImage(bmp);

        //    //使用ClearType字体功能
        //    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        //    g.FillRectangle(new SolidBrush(backColor), rect);
        //    //g.DrawString(text, font, Brushes.Black, rect, format);
        //    TextRenderer.DrawText(g, text, font, rect, fontColor);
        //    //bmp.Save("fontphoto");
        //    return bmp;
        //}

        ///// <summary>
        ///// 扩展BipMap的大小
        ///// </summary>
        ///// <param name="isUsePercent"> if true, 0 < pamrams < 1 of new size</param>
        //public static Bitmap ExtendBitMap(Bitmap bmp, float up, float down, float left, float right, bool isUsePercent = false)
        //{
        //    if (null == bmp)
        //    {
        //        return null;
        //    }
        //    Rect rect = new Rect(0, 0, bmp.Width, bmp.Height);
        //    rect = rect.Expand(up, down, left, right, isUsePercent);
        //    Bitmap newBmp = new Bitmap(FloatCeilingToInt(rect.width), FloatCeilingToInt(rect.height));
        //    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newBmp);
        //    g.FillRectangle(new SolidBrush(bmp.GetPixel(0, 0)), 0, 0, newBmp.Width, newBmp.Height);

        //    left = isUsePercent ? rect.width * left : left;
        //    up = isUsePercent ? rect.height * up : up;
        //    g.DrawImage(bmp, left, up, bmp.Width, bmp.Height);
        //    return newBmp;
        //}

        //public static Texture2D ImageToTexture2D(System.Drawing.Image imgOrg, bool isDeleteOrgin = false)
        //{
        //    if (null == imgOrg)
        //    {
        //        return null;
        //    }

        //    Texture2D ret = new Texture2D(imgOrg.Width, imgOrg.Height);
        //    if (ret.LoadImage(ImageToByteArrayDefault(imgOrg, isDeleteOrgin)))
        //    {
        //        return ret;
        //    }
        //    return null;
        //}

        //static void DestroyDisposableObj(IDisposable obj)
        //{
        //    if (obj != null)
        //    {
        //        obj.Dispose();
        //    }
        //    obj = null;
        //}

        //public static byte[] ImageToByteArrayDefault(System.Drawing.Image imageOrg, bool isDeleteOrgin = false)
        //{
        //    return ImageToByteArray(imageOrg, System.Drawing.Imaging.ImageFormat.Png, isDeleteOrgin);
        //}

        //public static byte[] ImageToByteArray(System.Drawing.Image imageOrg, System.Drawing.Imaging.ImageFormat saveFormat, 
        //    bool isDeleteOrgin = false)
        //{
        //    if (null == imageOrg)
        //    {
        //        return null;
        //    }
        //    using (var ms = new MemoryStream())
        //    {
        //        imageOrg.Save(ms, saveFormat);
        //        if (isDeleteOrgin)
        //        {
        //            DestroyDisposableObj(imageOrg);
        //        }
        //        return ms.ToArray();
        //    }
        //}

        //public static System.Drawing.Image ByteArrayToImage(byte[] byteArrayOrg)
        //{
        //    if (null == byteArrayOrg)
        //    {
        //        return null;
        //    }
        //    using (MemoryStream ms = new MemoryStream(byteArrayOrg))
        //    {
        //        System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);
        //        return returnImage;
        //    }
        //}

        //public static Texture ByteArrayToTexture(byte[] byteArrayOrg)
        //{
        //    if (null == byteArrayOrg)
        //    {
        //        return null;
        //    }
        //    using (MemoryStream ms = new MemoryStream(byteArrayOrg))
        //    {
        //        using (System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms))
        //        {
        //            return ImageToTexture2D(newImage, false);
        //        }
        //    }
        //}
        //#endregion

        //#region Unity

        ///// <summary>
        ///// 遍历Transform
        ///// </summary>
        ///// <param name="tf"></param>
        ///// <param name="Act">(Transform tf, Transform tfParent)</param>
        //public static void RecurTraverseTransform(Transform tf, Action<Transform, Transform> Act)
        //{
        //    int count = tf.childCount;
        //    for (int i = 0; i < count; i++)
        //    {
        //        var child = tf.GetChild(i);
        //        if (Act != null)
        //        {
        //            Act(child, tf);
        //        }
        //        RecurTraverseTransform(child, Act);
        //    }
        //}

        ///// <summary>
        ///// need test
        ///// </summary>
        ///// <param name="posOrg"> execute with no change </param>
        ///// <param name="posTarget"></param>
        ///// <param name="distance"></param>
        ///// <returns></returns>
        //static Vector3 GetPositionMoveTowardsTarget(this Vector3 posOrg, Vector3 posTarget, float distance)
        //{
        //    posOrg += (posTarget - posOrg).normalized * distance;
        //    return posOrg;
        //}

        ///// <summary>
        ///// need test
        ///// </summary>
        ///// <param name="posOrg">  execute with no change </param>
        ///// <param name="distance"></param>
        ///// <returns></returns>
        //static Vector3 GetPositionMoveTowardsMainCamera(this Vector3 posOrg, float distance)
        //{
        //    posOrg -= Camera.main.transform.forward * distance;
        //    return posOrg;
        //}
        //#endregion


        #region File

        public static byte[] LoadFile(FileInfo fInfo)
        {
            return LoadFile(fInfo.FullName);
        }
        public static byte[] LoadFile(string path) //@path)
        {
            if (File.Exists(path))
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    //创建文件长度缓冲区
                    byte[] bytes = new byte[fileStream.Length];
                    fileStream.Seek(0, SeekOrigin.Begin);
                    //读取文件
                    fileStream.Read(bytes, 0, (int)fileStream.Length);
                    return bytes;
                }
            }
            else
            {
                return null;
            }
        }

        public static string LoadFile2String(string path)
        {
            if (File.Exists(path))
            {
                return File.ReadAllText(path);
            }
            else
            {
                return string.Empty;
            }
        }

        public static void Save2File(string path, byte[] fileBytes, bool isAppend = false)
        {
            DirectoryExtention.TryCreateDirectory(path, true);
            using (FileStream fs = new FileStream(path, isAppend ? FileMode.Append : FileMode.OpenOrCreate))
            {
                if (!isAppend)
                {
                    fs.SetLength(0);
                }
                fs.Write(fileBytes, 0, fileBytes.Length);
            }
        }

        public static void Save2File(string path, string strFile, bool isAppend = false)
        {
            DirectoryExtention.TryCreateDirectory(path, true);
            using (FileStream fs = new FileStream(path, isAppend ? FileMode.Append : FileMode.OpenOrCreate))
            {
                if (!isAppend)
                {
                    fs.SetLength(0);
                }
                using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.Write(strFile);
                }
            }
        }

        public static bool IsDirExists(string path)
        {
            return Directory.Exists(path);
        }

        public static bool IsFileExists(string path)
        {
            return File.Exists(path);
        }

        public static FileInfo[] GetDirFileInfos(string path, bool isSubDir = false, string searchPattern = "*.*")
        {
            return DirectoryInfoExtention.GetDirFileInfos(path, isSubDir, searchPattern);
        }

        /// <summary>
        /// fileInfos必须是同级目录同类型文件(文件名不可重复)
        /// </summary>
        /// <param name="fileInfos"></param>
        /// <param name="dicName2FileInfo"></param>
        public static Dictionary<string, FileInfo> FileInfos2Dic(IEnumerable<FileInfo> fileInfos, 
            Dictionary<string, FileInfo> dicName2FileInfo = null, PathExtention.CaseType extType = PathExtention.CaseType.Lower)
        {
            if (null == fileInfos)
            {
                return null;
            }

            if (null == dicName2FileInfo)
            {
                dicName2FileInfo = new Dictionary<string, FileInfo>();
            }

            var it = fileInfos.GetEnumerator();
            while (it.MoveNext())
            {
                var item = it.Current;
                var key = PathExtention.ExtCaseChange(item.Name, extType);
                if (dicName2FileInfo.ContainsKey(key))
                {
                    //Debug.LogError("存在不同路径同名文件!!");
                }
                dicName2FileInfo.AddOrReplace(key, item);
            }

            return dicName2FileInfo;
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }

        #endregion
    }

    /// <summary>
    /// 线程安全请使用 ConcurrentDictionary<TKey, TValue> 类 (above .NET 4.0)
    /// </summary>
    public static class DictionaryExtention
    {
        /// <summary>
        /// 尝试直接通过key直接获取TValue的值 : 如果不存在, 返回defaultValue
        /// </summary>
        public static TValue TryGetReturnValue<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
        {
            TValue ret;
            if (!dic.TryGetValue(key, out ret))
            {
                return defaultValue;
            }
            return ret;
        }

        /// <summary>
        /// 尝试获取给定key的value值,如果没有key,则建立默认value的指定key pair
        /// </summary>
        /// <returns>获取到的value值或默认value值</returns>
        public static TValue ForceGetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
            where TValue : new()
        {
            var value = dic.TryGetReturnValue(key, defaultValue);
            if (value == null)
            {
                value = new TValue();
                dic.AddOrReplace(key, value);
            }
            return value;

        }

        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAddNoReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (!dict.ContainsKey(key))
            {
                dict.Add(key, value);
            }
            return dict;
        }
        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// </summary>
        /// <param name="isReplaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> dictValues,
            bool isReplaceExisted)
        {
            if (null == dictValues || null == dict)
            {
                return null;
            }
            var it = dictValues.GetEnumerator();
            while (it.MoveNext())
            {
                var item = it.Current;
                if (isReplaceExisted)
                {
                    dict.AddOrReplace(item.Key, item.Value);
                }
                else
                {
                    dict.TryAddNoReplace(item.Key, item.Value);
                }
            }
            return dict;
        }

        /// <summary>
        /// 向字典中批量删除键值对
        /// </summary>
        public static Dictionary<TKey, TValue> RemoveRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> dictValues)
        {
            if (null == dictValues || null == dict)
            {
                return null;
            }
            var it = dictValues.GetEnumerator();
            while (it.MoveNext())
            {
                var item = it.Current;
                dict.Remove(item.Key);
            }
            return dict;
        }
    }

    public static class ArrayExtention
    {
        /// <summary>
        /// 尝试直接通过index直接获取T : 如果不存在, 返回defaultValue
        /// </summary>
        public static T TryGetReturnValue<T>(this T[] ary, int index, T defaultValue = default(T))
        {
            if (index < 0 || index >= ary.Length)
            {
                return defaultValue;
            }
            return (T)ary.GetValue(index);
        }
    }

    public static class ListExtention
    {
        /// <summary>
        ///  尝试直接通过index直接获取T : 如果不存在, 返回defaultValue
        /// </summary>
        /// <returns></returns>
        public static T TryGetReturnValue<T>(this List<T> list, int index, T defaultValue = default(T))
        {
            if (null == list)
            {
                return defaultValue;
            }
            if (index < 0 || index >= list.Count)
            {
                return defaultValue;
            }
            return (T)(list[index]);
        }

        public static void Add<T>(this List<T> list, params T[] items)
        {
            for (int i = 0; i < items.Length; i++)
            {
                list.Add(items[i]);
            }
        }
    }

    public static class StackExtention
    {
        /// <summary>
        /// 如果Stack里没有内容(Count为0), 返回defaultValue而不抛出异常
        /// </summary>
        public static T SafePeek<T>(this Stack<T> stack, T defaultValue = default(T))
        {
            if (stack.Count == 0)
            {
                return defaultValue;
            }
            return stack.Peek();
        }
    }

    public static class StringExtention
    {
        public static string TrimLeft(this string str, int count)
        {
            if (count > str.Length)
            {
                count = str.Length;
            }
            return str.Remove(0, count);
        }

        public static string TrimRight(this string str, int count)
        {
            if (count > str.Length)
            {
                count = str.Length;
            }
            return str.Remove(str.Length - count, count);
        }
    }

    public static class StringBuilderExtention
    {
        public static void Append(this StringBuilder sb, params string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                sb.Append(strs[i]);
            }
        }

        public static void Clear(this StringBuilder sb)
        {
            sb.Length = 0;
        }
    }
    #region File Extention
    //public static class FileInfoExtention
    //{
    //    public static byte[] GetFileBytes(this FileInfo fInfo)
    //    {
    //        return UtilsCommonS.LoadFile(fInfo.FullName);
    //    }

    //    public static string GetFileCrc(this FileInfo fInfo)
    //    {
    //        var fBuf = fInfo.GetFileBytes();
    //        if (fBuf != null)
    //        {
    //            return Crc32.CountCrcRetString(fBuf);
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //}

    public static class DirectoryExtention
    {
        /// <summary>
        /// CreateDirectory if the Directory of the path is not exists
        /// </summary>
        public static void TryCreateDirectory(string path, bool isFilePath = false)
        {
            var folderPath = path;
            if (isFilePath)
            {
                folderPath = Path.GetDirectoryName(path);
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
        }

        public static void CloneDirectory(string srcDirPath, string destDirPath, bool isOverWrite = true)
        {
            if (Directory.Exists(srcDirPath))
            {
                TryCreateDirectory(destDirPath);
                FileSystemInfo[] srcFSysInfos = DirectoryInfoExtention.GetDirFileSysInfos(srcDirPath, "*.*");
                for (int i = 0; i < srcFSysInfos.Length; i++)
                {
                    FileSystemInfo srcFSys = srcFSysInfos[i];
                    string destName = Path.Combine(destDirPath, srcFSys.Name);

                    if (srcFSys.Attributes == FileAttributes.Directory)
                    {
                        //DirectoryInfo dirInfo = srcFSys as DirectoryInfo;
                        CloneDirectory(srcFSys.FullName, destName);
                    }
                    else
                    {
                        File.Copy(srcFSys.FullName, destName, isOverWrite);
                    }
                }
            }
        }

        /// <summary>
        /// 指定文件拷贝到目录
        /// </summary>
        public static void CloneFiles(FileSystemInfo[] srcInfos, string destDirPath, bool isOverWrite = true)
        {
            for (int i = 0; i < srcInfos.Length; i++)
            {
                FileSystemInfo srcFSys = srcInfos[i];
                string destName = Path.Combine(destDirPath, srcFSys.Name);

                if (srcFSys.Attributes == FileAttributes.Directory)
                {
                    DirectoryInfo dirInfo = srcFSys as DirectoryInfo;

                    //CloneDirectory(srcFSys.FullName, destName);
                }
                else
                {
                    File.Copy(srcFSys.FullName, destName, isOverWrite);
                }
            }
        }

    }

    public static class DirectoryInfoExtention
    {
        /// <summary>
        /// 获取文件的FileInfo
        /// </summary>
        public static FileInfo[] GetDirFileInfos(string path, bool isSubDir = false, string searchPattern = "*.*")
        {
            DirectoryInfo dirInfo = CreateDirWithExistPath(path);
            if (null == dirInfo)
            {
                return null;
            }

            return dirInfo.GetFiles(searchPattern, isSubDir ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strExtensions"> split by |, no space </param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> GetDirFileInfosByExtSplit(string path, SearchOption searchOption, string strExtensions)
        {
            return GetDirFileInfosByExts(path, searchOption, strExtensions.Split('|'));
        }

        /// <summary>
        /// fast, wildcard is not supported
        /// </summary>
        public static IEnumerable<FileInfo> GetDirFileInfosByExts(string path, SearchOption searchOption, params string[] extensions)
        {
            DirectoryInfo dirInfo = CreateDirWithExistPath(path);
            if (null == dirInfo)
            {
                return null;
            }

            var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);

            //var allFile = dirInfo.GetFiles("*", searchOption);
            //IEnumerable<FileInfo> ret = from f in allFile
            //                            where allowedExtensions.Contains(f.Extension)
            //                            select f;
            //var ary = ret.ToArray();
            
            return dirInfo.GetFiles("*", searchOption).
                          Where(f => allowedExtensions.Contains(f.Extension));
        }

        // Regex version
        public static IEnumerable<FileInfo> GetDirFileInfosByRegex(string path,
                            string searchPatternExpression = "",
                            SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            DirectoryInfo dirInfo = CreateDirWithExistPath(path);
            if (null == dirInfo)
            {
                return null;
            }

            Regex reSearchPattern = new Regex(searchPatternExpression, RegexOptions.IgnoreCase);
            return dirInfo.GetFiles("*", searchOption)  //EnumerateFiles
                            .Where(f =>
                                     reSearchPattern.IsMatch(Path.GetExtension(f.Name)));
        }

        /// <summary>
        /// 获取文件夹(不包括子文件夹)下的 文件/文件夹的FileSystemInfo
        /// </summary>
        public static FileSystemInfo[] GetDirFileSysInfos(string path, string searchPattern = "*.*")
        {
            DirectoryInfo dirInfo = CreateDirWithExistPath(path);
            if (null == dirInfo)
            {
                return null;
            }

            return dirInfo.GetFileSystemInfos(searchPattern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="strExtensions"> split by |, no space </param>
        /// <returns></returns>
        public static IEnumerable<FileSystemInfo> GetDirFileSysInfosByExtSplit(string path, string strExtensions)
        {
            return GetDirFileSyInfosByExts(path, strExtensions.Split('|'));
        }

        /// <summary>
        /// fast, wildcard is not supported
        /// </summary>
        public static IEnumerable<FileSystemInfo> GetDirFileSyInfosByExts(string path, params string[] extensions)
        {
            DirectoryInfo dirInfo = CreateDirWithExistPath(path);
            if (null == dirInfo)
            {
                return null;
            }
            var allowedExtensions = new HashSet<string>(extensions, StringComparer.OrdinalIgnoreCase);

            return dirInfo.GetFileSystemInfos()
                          .Where(f => allowedExtensions.Contains(f.Extension.ToLower()));
        }

        /// <summary>
        /// return null if not exist
        /// </summary>
        public static DirectoryInfo CreateDirWithExistPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            if (!Directory.Exists(path))
            {
                return null;
            }

            return new DirectoryInfo(path);
        }
    }

    public static class PathExtention
    {
        public enum CaseType
        {
            Keep,
            Lower,
            Upper
        }
        /// <summary>
        /// 全部大小写用StringComparer.OrdinalIgnoreCase(IEqualityComparer)解决
        /// 这里解决name不变,后缀名大小写统一的问题(文件名放dic)
        /// </summary>
        /// <param name="caseType"></param>
        public static string ExtCaseChange(string path, CaseType caseType = CaseType.Lower)
        {
            string nameExt = Path.GetFileName(path);
            string ext = Path.GetExtension(nameExt);
            if (!string.IsNullOrEmpty(ext))
            {
                string extChange = string.Empty;
                switch (caseType)
                {
                    case CaseType.Lower:
                        extChange = ext.ToLower();
                        break;
                    case CaseType.Upper:
                        extChange = ext.ToUpper();
                        break;
                    default:
                        break;
                }
                if (ext != extChange)
                {
                    return Path.ChangeExtension(path, extChange);
                }
            }
            return path;
        }

        public static string Combine(string sourcePath, params string[] strs)
        {
            for (int i = 0; i < strs.Length; i++)
            {
                sourcePath = Path.Combine(sourcePath, strs[i]);
            }
            return sourcePath;
        }

        public static string GetPureExtension(string path)
        {
            string ext = Path.GetExtension(path);

            if (ext.StartsWith("."))
            {
                return ext.TrimLeft(1);
            }
            return ext;
        }
    }

    #endregion file

    //public static class UnityEngineExtention
    //{
    //    /// <summary>
    //    /// Expand rect size
    //    /// </summary>
    //    /// <param name="rect"></param>
    //    /// <param name="up"></param>
    //    /// <param name="down"></param>
    //    /// <param name="left"></param>
    //    /// <param name="right"></param>
    //    /// <param name="isUsePercent"> if true, 0 < pamrams < 1 of new size</param>
    //    /// <returns></returns>
    //    public static Rect Expand(this Rect rect, float up, float down, float left, float right, bool isUsePercent = false)
    //    {
    //        if (isUsePercent)
    //        {
    //            // perUp + perDown + rect.height/newHeight = 1
    //            float newHeight = rect.height / (1 - up - down);
    //            float newWidth = rect.width / (1 - left - right);

    //            rect.x -= (newWidth - rect.width) / 2;
    //            rect.y -= (newHeight - rect.height) / 2;
    //            rect.width = newWidth;
    //            rect.height = newHeight;
    //        }
    //        else
    //        {
    //            rect.x -= left;
    //            rect.y -= up;
    //            rect.width += left + right;
    //            rect.height += up + down;
    //        }

    //        return rect;
    //    }
    //}
}



