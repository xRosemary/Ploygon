using System;
using UnityEngine;
using UnityEngine.UI;

public class Program : MonoBehaviour
{
    int[,,] dp = new int[1000, 1000, 2];
    int[] val = new int[1000];
    char[] sign = new char[1000];
    char[] order = new char[1000];
    int ordord = 0;
    int[] ord = new int[1000];

    //v     定点数值数组
    //s     边字符串数组
    //run   顺序返回删除边的下标数组
    private int[] ans;
    float time = 0;
    int count = 0;
    int n;
    public void AutoDelete()
    {
        n = Create.edgeArr.Count;
        int[] v = new int[n];
        char[] s = new char[n];

        for (int i = 0; i < n; i++)
        {
            v[i] = Create.vertexVal[i];
            s[i] = Create.edgeVal[i][0];
        }
        string str = "";
        foreach (int i in v)
        {
            str += i + ",";
        }
        Debug.Log(str);
        str = "";
        foreach (char i in s)
        {
            str += i + ",";
        }
        Debug.Log(str);

        ans = run(v, s);

        str = "";
        foreach (int i in ans)
        {
            str += i + ",";
        }
        Debug.Log(str);



        Delete.DeleteEdge(ans[0]);

        int head = 0;
        for (int i = 1; i < n; i++)
        {
            if(ans[i] < ans[0])
            {
                ans[i] += 1000;
            }
            else if(ans[i] > ans[0])
            {
                ans[i] = ans[i] - ans[0] - 1;
                head++;
            }
        }

        for (int i = 1; i < n; i++)
        {
            if (ans[i] >= 1000)
            {
                ans[i] = ans[i] - 1000 + head;
            }
        }
    }

    private void Update()
    {
        if(time > 2 && count < n)
        {
            time = 0;
            if (count == 0)
            {
                count++;
                return;
            }
            Delete.DeleteEdge(ans[count]);
            for (int i = 0; i < ans.Length; i++)
            {
                if(ans[i] > ans[count])
                    ans[i]--;
            }
            count++;
        }
        else
        {
            time += Time.deltaTime;
        }
    }

    public int[] run(int[] v, char[] s){
        int n = v.Length;
        val = new int[2*n];
        sign = new char[2*n];
        for(int i=0;i<n;i++){
            val[i] = v[i];
            sign[i] = s[i];
            val[i+n] = v[i];
            sign[i+n] = s[i];
        }
        for(int i=0;i<2*n;i++){
            dp[i, i, 0] = val[i];
            dp[i, i, 1] = val[i];
        }	
        for(int len=1;len<n;len++){
            for(int i=0;i<n;i++){
                int max=int.MinValue,min=int.MaxValue,tempmax,tempmin;
                for(int k=i;k<i+len;k++){
                    tempmax = countMax(i,k,len,sign[k]);
                    tempmin = countMin(i,k,len,sign[k]);
                    if(tempmax>max){
                        max = tempmax;
                    }
                    if(tempmin<min){
                        min = tempmin;
                    }
                }
                dp[i, i+len, 0] = max;
                dp[i, i+len, 1] = min;
                if(i+len+n<2*n){
                    dp[i+n, i+n+len, 0] = dp[i, i+len, 0];
                    dp[i+n, i+n+len, 1] = dp[i, i+len, 1];
                }
            }
        }
        int maxline = 0,maxn = int.MinValue;
        for(int i=0;i<n;i++){
            if(dp[i, i+n-1, 0]>maxn){
                maxn = dp[i, i+n-1, 0];
                maxline = i;
            }
        }
        if (maxline == 0)
        {
            Debug.Log("最优值为" + maxn + ",删除" + (maxline + n) + "号边得到" + "\n");
        }
        else
            Debug.Log("最优值为" + maxn + ",删除" + maxline + "号边得到\n");
        // for(int i = 0; i < 2*n; i++){
        //     for(int j = 0; j < 2*n; j++){
        //         Console.Write(dp[i, j, 0]+"\t");
        //     }
        //     Console.Write("\n");
        // }
        getord(maxline,maxn,n);
        // Console.WriteLine("删除边的顺序为:");

        int[] ret = new int[n];
        if(maxline==0) ret[0] = maxline+n-1;
        else ret[0] = maxline-1;
        for(int i=n-2;i>=0;i--){
            ret[n-2-i+1] = ord[i]%n;
        }
        return ret;
    }
    int countMax(int i, int k, int len, char c){
        int max = int.MinValue, temp;
        if(c=='+'){
            return dp[i, k, 0] + dp[k+1, i+len, 0];
        }
        else{
            temp = dp[i, k, 0]*dp[k+1, i+len, 0];
            if(temp>max){
                max = temp;
            }
            temp = dp[i, k, 0]*dp[k+1, i+len, 1];
            if(temp>max){
                max = temp;
            }
            temp = dp[i, k, 1]*dp[k+1, i+len, 1];
            if(temp>max){
                max = temp;
            }
            temp = dp[i, k, 1]*dp[k+1, i+len, 0];
            if(temp>max){
                max = temp;
            }
            return max;
        }
    }
    int countMin(int i,int k,int len,char c){//用于计算最小值 
        int min = int.MaxValue,temp;
        if(c=='+'){
            return dp[i, k, 1]+dp[k+1, i+len, 1];
        }
        else {
            temp = dp[i, k, 1]*dp[k+1, i+len, 1];
            if(temp<min){
                min = temp;
            }
            temp = dp[i, k, 0]*dp[k+1, i+len, 0];
            if(temp<min){
                min = temp;
            }
            temp = dp[i, k, 1]*dp[k+1, i+len, 0];
            if(temp<min){
                min = temp;
            }
            temp = dp[i, k, 0]*dp[k+1, i+len, 1];
            if(temp<min){
                min = temp;
            }
            return min;
        }
    }
    void getord(int maxline,int maxn,int n){
        int k;
        int len = n - 1;
        if(n <= 1){
            return;
        }
        else{
            for(k=maxline;k<maxline+len;k++){
                if(countMax(maxline,k,len,this.sign[k]) == maxn){
                    this.order[this.ordord] = this.sign[k];
                    this.ord[this.ordord] = k;
                    this.ordord++;
                    break;
                }
            }
            this.getord(maxline, this.dp[maxline, k, 0], k-maxline+1);
            // Console.WriteLine("k+1\t"+ (k+1) +"\tdp[k+2, maxline+n-1, 0]\t"+this.dp[k+2, maxline+n-1, 0]+ "\tmaxline+n-1\t" + (maxline+n-1) + "\tmaxline\t" + maxline +"\n");
            this.getord(k+1,this.dp[k+2, maxline+n-1, 0], maxline+n-1-k-1+1);
        }
    }
}
