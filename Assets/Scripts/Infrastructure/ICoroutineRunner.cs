using System.Collections;
using UnityEngine;

namespace TenTen
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}