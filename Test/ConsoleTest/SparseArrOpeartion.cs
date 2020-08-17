using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest
{
    public class SparseArrOpeartion
    {
        /// <summary>
        /// 根据数组生成稀疏数组
        /// </summary>
        /// <param name="originalArray"></param>
        /// <returns></returns>
        public static int[,] OriginalToSparseArray(int[,] originalArray)
        {
            Write(originalArray);
            int cout = 0;//记录原始数组有效值的个数
            foreach (var cell in originalArray)
            {
                if (cell != 0)
                {
                    cout++;
                }
            }
            int rowNum = originalArray.GetLength(0); //记录原始数组的行数,第一维的长度
            int colNum = originalArray.GetLength(1); //记录原始数组的列数,第二维的长度 

            int sparseColNum = 3;
            int sparseRowNum = cout + 1;//第一行记录着原始数组的行列和有效数据，所以要加1
            int[,] sparseArray = new int[sparseRowNum, sparseColNum];
            int identitySparseRowNum = 0;  //记录稀疏数组使用的行号
            sparseArray[identitySparseRowNum, 0] = rowNum;
            sparseArray[identitySparseRowNum, 1] = colNum;
            sparseArray[identitySparseRowNum, 2] = cout;
            identitySparseRowNum++;
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    if (originalArray[i, j] != 0)
                    {
                        sparseArray[identitySparseRowNum, 0] = i;
                        sparseArray[identitySparseRowNum, 1] = j;
                        sparseArray[identitySparseRowNum, 2] = originalArray[i, j];
                        identitySparseRowNum++;
                    }
                }
            }
            Write(sparseArray);
            return sparseArray;
        }

        /// <summary>
        /// 根据稀疏数组转换为二维原始数组
        /// </summary>
        /// <param name="sparseArray"></param>
        /// <returns></returns>
        public static int[,] SparseToOriginalArray(int[,] sparseArray)
        {
            Write(sparseArray);
            int[,] restArray = new int[sparseArray[0, 0], sparseArray[0, 1]];
            for (int i = 1; i <= sparseArray[0, 2]; i++) // 第一行记录的是原始数组的信息
            {
                // row = i,0  col = i,1  value =i,2
                restArray[sparseArray[i, 0], sparseArray[i, 1]] = sparseArray[i, 2];
            }
            Write(restArray);
            return restArray;
        }

        /// <summary>
        /// 输出数组查看,用于 调试
        /// </summary>
        /// <param name="array"></param>
        private static void Write(int[,] array)
        {
            int rowNum = array.GetLength(0); //记录原始数组的行数,第一维的长度
            int colNum = array.GetLength(1); //记录原始数组的列数,第二维的长度 
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < colNum; j++)
                {
                    Console.Write($"{array[i, j].ToString().PadLeft(2, ' ')}  ");
                }
                Console.WriteLine(" ");
            }
            Console.WriteLine(" ====输出完毕=====");
        }
    }
}
