﻿Module Module_lang
    Public local_lang As String = "en"
    Public Sub change_lang(ByVal lang As String)
        Select Case lang
            Case "ch"
                MainWindow.LoadTreesToolStripMenuItem.Text = "载入树集合"
                MainWindow.FileToolStripMenuItem.Text = "文件"
                MainWindow.LoadTreesDataToolStripMenuItem.Text = "标准载入"
                MainWindow.QuickLoadToolStripMenuItem.Text = "快速载入(Beast,mrBayes)"
                MainWindow.LoadOneTreeToolStripMenuItem.Text = "载入单棵树"
                MainWindow.AddTreesDataToolStripMenuItem.Text = "添加树"
                MainWindow.LoadDistrutionToolStripMenuItem.Text = "加载性状"
                MainWindow.LoadFinalTreeToolStripMenuItem.Text = "载入合并树"
                MainWindow.LoadConsensusTreeToolStripMenuItem.Text = "用户自定义树"
                MainWindow.MakeFinalTreeToolStripMenuItem1.Text = "生成严格一致树"
                MainWindow.OpenResultToolStripMenuItem.Text = "载入结果"
                MainWindow.SaveToolStripMenuItem.Text = "保存"
                MainWindow.SaveResultToolStripMenuItem.Text = "保存结果"
                MainWindow.SaveDistributionToolStripMenuItem.Text = "保存性状"
                MainWindow.SaveLogToolStripMenuItem.Text = "保存日志"
                MainWindow.ExportToolStripMenuItem.Text = "导出树"
                MainWindow.FormatedTreeToolStripMenuItem.Text = "严格一致树"
                MainWindow.TreeDataSetToolStripMenuItem.Text = "去抛弃后的树"
                MainWindow.RandomTreesToolStripMenuItem.Text = "随机树"
                MainWindow.CloseDataToolStripMenuItem.Text = "关闭现有文件"
                MainWindow.ExitToolStripMenuItem.Text = "退出"
                MainWindow.GraphicToolStripMenuItem.Text = "图像"
                MainWindow.TreeViewToolStripMenuItem.Text = "查看树"
                MainWindow.PiePictureToolStripMenuItem.Text = "查看节点"
                MainWindow.TracerViewToolStripMenuItem.Text = "查看Bayes概率分布"
                MainWindow.TraitsViewToolStripMenuItem.Text = "查看性状(BayesTraits)"
                MainWindow.ProcessToolStripMenuItem.Text = "祖先性状重构"
                MainWindow.ModelTestToolStripMenuItem.Text = "模型检测"
                MainWindow.BioGeoBEARSToolStripMenuItem2.Text = "BioGeoBEARS六模型检测"
                MainWindow.StatisticalMethodsToolStripMenuItem.Text = "树集合"
                MainWindow.OriginalMethodsToolStripMenuItem.Text = "一致树"
                MainWindow.TraitsEvolutionToolStripMenuItem.Text = "性状演化"
                MainWindow.ComparisonToolStripMenuItem.Text = "比较"
                MainWindow.TreeClusterToolStripMenuItem.Text = "树聚类"
                MainWindow.StatesVsTreesToolStripMenuItem.Text = "性状和树比较"
                MainWindow.OtherToolStripMenuItem.Text = "工具"
                MainWindow.InstallReinstallToolStripMenuItem.Text = "安装三方库"
                MainWindow.RemoveOutgroupToolStripMenuItem.Text = "去除选中的类群"
                MainWindow.CombineResultsToolStripMenuItem.Text = "合并结果"
                MainWindow.OmittedTaxaToolStripMenuItem1.Text = "添加缺失的类群"
                MainWindow.AddTreeLengthToolStripMenuItem.Text = "添加支长"
                MainWindow.TreeTimeMultiplierToolStripMenuItem.Text = "支长放大"
                MainWindow.ConvertStatesToolStripMenuItem.Text = "转化性状"
                MainWindow.DatingToolStripMenuItem.Text = "分子钟"
                MainWindow.BatchToolToolStripMenuItem.Text = "批量工具"
                MainWindow.HelpToolStripMenuItem.Text = "帮助"
                MainWindow.AboutToolStripMenuItem.Text = "关于"
                MainWindow.DebugToolStripMenuItem.Text = "测试"
                MainWindow.GroupBox1.Text = "树"
                MainWindow.Label3.Text = "二歧树"
                MainWindow.Label1.Text = "树总量"
                MainWindow.Label2.Text = "舍弃树"
                MainWindow.CheckBox3.Text = "随机树"
                MainWindow.Label4.Text = "当前的合并树："
                MainWindow.Button1.Text = "检查配置"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
                'MainWindow.DebugToolStripMenuItem.Text = "测试"
            Case Else

        End Select

    End Sub
End Module
