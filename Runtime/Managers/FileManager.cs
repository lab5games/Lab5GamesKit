using System;
using System.IO;
using UnityEngine;

namespace Lab5Games
{
    public static class FileManager 
    {
        public static string GetAbsolutePath(string path)
        {
#if UNITY_EDITOR
            return Path.Combine(Application.dataPath + @"/../Library", path);
#else
            return Path.Combine(Application.persistentDataPath, path);
#endif
        }

        public static bool CreateFile(string relative_path)
        {
            string path = GetAbsolutePath(relative_path);

            try
            {
                if(File.Exists(path))
                {
                    GLogger.LogAsType($"Failed to create {path} file, it is already existed.", LogType.Error);
                    return false;
                }

                File.Create(path);
                GLogger.LogAsType("Create file successed.", LogType.Log);

                return true;
            }
            catch(Exception e)
            {
                GLogger.LogAsType($"Failed to create {path} file with exception: {e}", LogType.Exception);
                return false;
            }
        }

        public static bool DeleteFile(string relative_path)
        {
            string path = GetAbsolutePath(relative_path);

            try
            {
                File.Delete(path);
                GLogger.LogAsType("Delete file successed.", LogType.Log);

                return true;
            }
            catch(Exception e)
            {
                GLogger.LogAsType($"Failed to delete {path} file with exception: {e}", LogType.Exception);
                return false;
            }
        }

        public static bool MoveFile(string src_relative_path, string dest_relateive_path)
        {
            string src_path = GetAbsolutePath(src_relative_path);
            string dest_path = GetAbsolutePath(dest_relateive_path);

            try
            {
                if(!File.Exists(src_path))
                {
                    GLogger.LogAsType($"Failed to move {src_path} file that does not exist", LogType.Error);
                    return false;
                }

                if(File.Exists(dest_path))
                {
                    File.Delete(dest_path);
                }

                File.Move(src_path, dest_path);
                GLogger.LogAsType("Move file successed", LogType.Log);

                return true;
            }
            catch(Exception e)
            {
                GLogger.LogAsType($"Failed to move file from {src_path} to {dest_path}, and with exception: {e}", LogType.Exception);
                return false;
            }
        }

        public static bool WriteFile(string relative_path, string content)
        {
            string path = GetAbsolutePath(relative_path);

            try
            {
                File.WriteAllText(path, content);
                GLogger.LogAsType("Write file successed.", LogType.Log);

                return true;
            }
            catch(Exception e)
            {
                GLogger.LogAsType($"Failed to write {path} file with exception: {e}", LogType.Exception);
                return false;
            }
        }

        public static bool ReadFile(string relative_path, out string content)
        {
            string path = GetAbsolutePath(relative_path);

            try
            {
                if(!File.Exists(path))
                {
                    content = null;
                    GLogger.LogAsType($"Failed to read {path} file that does not exist", LogType.Error);
                    return false;
                }

                content = File.ReadAllText(path);
                GLogger.LogAsType("Read file successed.", LogType.Log);

                return true;
            }
            catch(Exception e)
            {
                content = null;
                GLogger.LogAsType($"Failed to read {path} file with exception: {e}", LogType.Exception);
                return false;
            }
        }
    }
}
