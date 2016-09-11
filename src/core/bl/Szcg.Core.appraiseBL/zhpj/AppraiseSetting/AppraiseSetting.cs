using System;
using System.Collections.Generic;
using System.Text;
using bacgDL.zhpj;

namespace bacgBL.zhpj.AppraiseSetting
{
    public class  AppraiseSetting
    {
        public void AppraiseSettingAction()
        {
            //String forward = "list";
            //AppraiseSettingForm appraiseSettingForm = (AppraiseSettingForm) form;
            //String functionName = appraiseSettingForm.getFunctionName();
            //String functionData = appraiseSettingForm.getFunctionData();
            //appraiseSettingForm.setFunctionName("");
            //AppraiseSettingDAO appraiseSettingDAO = new AppraiseSettingDAO();
            //if ("PAGING".equalsIgnoreCase(functionName)) {
            //    try {
            //        appraiseSettingForm.getPagingInfo().setPageAction(functionData);
            //        appraiseSettingForm.getPagingInfo().changePage();
            //    } catch (Exception ex) {
            //        appraiseSettingForm.setFunctionName("ERROR");
            //        appraiseSettingForm.setFunctionData(ex.getMessage());
            //    } finally {
            //        String lastForward = (String) request.getSession()
            //                .getAttribute("lastForward");
            //        functionData = (String) request.getSession().getAttribute(
            //                "functionData");
            //        if ("LIST".equals(lastForward)) {
            //            functionName = "QUERY";
            //        }else if("ADDQUERY".equalsIgnoreCase(functionName)){
            //            functionName = "ADDQUERY";
            //        }else {
            //            functionName = "QUERY";
            //        }
            //    }
            //}

            //if("DEL".equalsIgnoreCase(functionName)){
            //    String id = functionData;
            //    try{
            //        if(appraiseSettingDAO.isBaseField(id)){
            //            appraiseSettingForm.setFunctionName("MESSAGE");
            //            appraiseSettingForm.setFunctionData("该字段为基础字段,不能删除!");	
            //        }else{
            //            appraiseSettingDAO.deleteField(id);
            //            appraiseSettingForm.setFunctionName("MESSAGE");
            //            appraiseSettingForm.setFunctionData("删除成功!");	
            //        }
            //    }catch(Exception ex){
            //        appraiseSettingForm.setFunctionName("ERROR");
            //        appraiseSettingForm.setFunctionData(ex.getMessage());	
            //    }
            //    functionName = "QUERY";
            //}

            ///*
            // * 改变字段顺序
            // */
            //if("CHANGEORDER_UP".equalsIgnoreCase(functionName)
            //        ||"CHANGEORDER_DOWN".equalsIgnoreCase(functionName)){
            //    String id = request.getParameter("id");
            //    String model = request.getParameter("model");
            //    String roleid = request.getParameter("roleid");
            //    String status = "";
            //    if("CHANGEORDER_UP".equalsIgnoreCase(functionName)){
            //        status = "UP";
            //    }else{
            //        status = "DOWN";
            //    }
            //    try{
            //        appraiseSettingDAO.orderField(model, id, status, roleid);
            //    }catch(Exception ex){
            //        ex.printStackTrace();
            //    }
            //    functionName="LIST";
            //}

            //if ("LIST".equalsIgnoreCase(functionName)
            //        ||"QUERY".equalsIgnoreCase(functionName)
            //        ||"FIRSTQUERY".equalsIgnoreCase(functionName)
            //        ||"FIRSTADDQUERY".equalsIgnoreCase(functionName)
            //        ||"ADDQUERY".equalsIgnoreCase(functionName)){
            //    PagingInfo pagingInfo = appraiseSettingForm.getPagingInfo();
            //    if ("LIST".equalsIgnoreCase(functionName)
            //        ||"FIRSTQUERY".equalsIgnoreCase(functionName)
            //        ||"FIRSTADDQUERY".equalsIgnoreCase(functionName)){
            //        appraiseSettingForm.getPagingInfo().setStartIndex(0);
            //        request.getSession().setAttribute("appraiseSetting_bak",
            //                appraiseSettingForm.getAppraiseSetting());
            //    }
            //    try {
            //        AppraiseSetting appraiseSetting = (AppraiseSetting) request.getSession().getAttribute(
            //                "appraiseSetting_bak");
            //        ArrayList list = new ArrayList();
            //        if("FIRSTADDQUERY".equalsIgnoreCase(functionName)
            //        ||"ADDQUERY".equalsIgnoreCase(functionName)){
            //            list = appraiseSettingDAO.getFieldList(appraiseSetting, 0, 1000);
            //        }else{
            //            list = appraiseSettingDAO.getFieldList(appraiseSetting,pagingInfo
            //                .getStartIndex(), pagingInfo.getPageSize());
            //        }
            //        pagingInfo.setTotalSize(appraiseSettingDAO.getResutlCount());
            //        appraiseSettingForm.setAppraiseList(list);
            //    } catch (Exception ex) {
            //        appraiseSettingForm.setFunctionName("ERROR");
            //        appraiseSettingForm.setFunctionData(ex.getMessage());
            //    }
            //    request.getSession().setAttribute("lastForward", functionName);
            //    if("ADDQUERY".equalsIgnoreCase(functionName)||"FIRSTADDQUERY".equalsIgnoreCase(functionName)){
            //        forward = "ADDQUERY";
            //    }else{
            //        forward = "list";
            //    }
            //}

            //if("BEFOREADD".equalsIgnoreCase(functionName)){
            //    forward = "ADD";
            //}

            //if("ADD".equalsIgnoreCase(functionName)){
            //    AppraiseSetting appraiseSetting = appraiseSettingForm.getAppraiseSetting();
            //    try{
            //        if(appraiseSettingDAO.isCodeNameExist(appraiseSetting.getCodeName())){
            //            appraiseSettingForm.setFunctionName("ERROR");
            //            appraiseSettingForm.setFunctionData("您输入的指标代号已存在,请重新输入!");
            //        }
            //        //判断表达式是否合法
            //        else if(!appraiseSettingDAO.validateExpress(appraiseSetting.getExpress())){
            //            appraiseSettingForm.setFunctionName("ERROR");
            //            appraiseSettingForm.setFunctionData("您输入的指标公式有误!请确认后重新输入!");
            //        }else{
            //            //if(!"personAppraise".equalsIgnoreCase(appraiseSetting.getModel())){
            //            //	appraiseSetting.setRoleid("");
            //            //}
            //        appraiseSettingDAO.insertAppraiseSetting(appraiseSetting);
            //        appraiseSettingForm.setFunctionName("MESSAGE");
            //        appraiseSettingForm.setFunctionData("记录添加成功!");
            //        }
            //    }catch(Exception ex){
            //        appraiseSettingForm.setFunctionName("ERROR");
            //        appraiseSettingForm.setFunctionData(ex.getMessage());
            //    }
            //    forward = "ADD";
            //}

            ///*
            // * 检查表达式的合法性
            // */
            //if("CHECKEXPRESS".equalsIgnoreCase(functionName)){
            //    String express = functionData;
            //    String res = "";
            //    if(!appraiseSettingDAO.validateExpress(express)){
            //         res="0";
            //    }else{
            //         res="1";
            //    }
            //    response.getWriter().print(res);
            //    response.getWriter().close();
            //    return null;
            //}

            //if("BEFOREMOD".equalsIgnoreCase(functionName)){
            //    String id = functionData;
            //    try{
            //        AppraiseSetting appraiseSetting = appraiseSettingDAO.getFieldDetail(id);
            //        String isDisplay = appraiseSetting.getIsDisplay();
            //        if("是".equalsIgnoreCase(isDisplay)){
            //            isDisplay = "on";
            //        }else{
            //            isDisplay = "";
            //        }
            //        appraiseSetting.setIsDisplay(isDisplay);
            //        String isBase = appraiseSetting.getIsBase();
            //        if("是".equalsIgnoreCase(isBase)){
            //            isBase = "on";
            //        }else{
            //            isBase = "";
            //        }
            //        appraiseSetting.setIsBase(isBase);

            //        String model = appraiseSetting.getModel();
            //        if("区域评价".equalsIgnoreCase(model)){
            //            model = "areaAppraise";
            //        }else if("部门评价".equalsIgnoreCase(model)){
            //            model = "departAppraise";
            //        }else if("岗位评价".equalsIgnoreCase(model)){
            //            model = "postAppraise";
            //        }else if("人员评价".equalsIgnoreCase(model)){
            //            model = "personAppraise";
            //        }
            //        appraiseSetting.setModel(model);
            //        appraiseSettingForm.setAppraiseSetting(appraiseSetting);
            //    }catch(Exception ex){
            //        appraiseSettingForm.setFunctionName("ERROR");
            //        appraiseSettingForm.setFunctionData(ex.getMessage());
            //    }
            //    forward = "MOD";
            //}

            //if("MOD".equalsIgnoreCase(functionName)){
            //    AppraiseSetting appraiseSetting = appraiseSettingForm.getAppraiseSetting();
            //    try{
            //        //if(!"personAppraise".equalsIgnoreCase(appraiseSetting.getModel())){
            //        //	appraiseSetting.setRoleid("");
            //        //}
            //        appraiseSettingDAO.updateAppraiseSetting(appraiseSetting);
            //        appraiseSettingForm.setFunctionName("MESSAGE");
            //        appraiseSettingForm.setFunctionData("记录修改成功!");
            //    }catch(Exception ex){
            //        appraiseSettingForm.setFunctionName("ERROR");
            //        appraiseSettingForm.setFunctionData(ex.getMessage());
            //    }
            //    forward = "MOD";
            //}

            //return mapping.findForward(forward);
        }
    }
}
