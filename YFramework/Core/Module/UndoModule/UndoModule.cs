using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public static class UndoModule
    {
        private static Stack<IUnDo> mUndoStack; //撤回模块栈
        private static Stack<IUnDo> mRedoStack;//反撤回模块栈
        private const int MAX_SIZE = 100;//撤回缓存最大数量
        private const int REMAIN_SIZE = 10;//清除后剩余的撤回对象个数
        /// <summary>
        /// 添加撤回对象
        /// </summary>
        /// <param name="undo"></param>
        public static void Add(IUnDo undo)
        {
            if (undo == null) return;
            if (mUndoStack == null) mUndoStack = new Stack<IUnDo>();
            if (mUndoStack.Count >= MAX_SIZE)
            {
                ClearGarbage();
            }
            mUndoStack.Push(undo);
            undo.Do();
        }

        /// <summary>
        /// 撤回
        /// </summary>
        public static void Undo() 
        {
            if (mUndoStack == null || mUndoStack.Count == 0) return;
            IUnDo tempUndo =  mUndoStack.Pop();
            if (tempUndo == null) return;
            if (mRedoStack == null) mRedoStack = new Stack<IUnDo>();
            mRedoStack.Push(tempUndo) ;
            tempUndo.Undo();
        }
        /// <summary>
        /// 反向撤回
        /// </summary>
        public static void Redo() 
        {

            if (mRedoStack == null || mRedoStack.Count == 0) return;
            IUnDo tempRedo = mRedoStack.Pop();
            if (tempRedo == null) return;
            mUndoStack.Push(tempRedo);
            tempRedo.Redo();
        }
        /// <summary>
        /// 清理撤回模块的数据
        /// </summary>
        public static void Clear() {
            if (mUndoStack != null) 
            {
                mUndoStack.Clear();
            }
            if (mRedoStack != null) 
            {
                mRedoStack.Clear();
            }
        }
        /// <summary>
        /// 清理缓存垃圾,保留REMAIN_SIZE个撤回对象，清理所有的反撤回对象
        /// </summary>
        private static void ClearGarbage() 
        {
            IUnDo[] tempUndos = new BaseUndo[REMAIN_SIZE];
            for (int i = 0; i < REMAIN_SIZE; i++)
            {
                tempUndos[i] = mUndoStack.Pop();
            }
            mUndoStack.Clear();
            for (int i = REMAIN_SIZE-1; i >=0; i--)
            {
                mUndoStack.Push(tempUndos[i]);
            }
            if (mRedoStack != null) mRedoStack.Clear();
        }
    }
}