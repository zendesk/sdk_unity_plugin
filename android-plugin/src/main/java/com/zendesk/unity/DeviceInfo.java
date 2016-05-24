package com.zendesk.unity;

import java.util.Map;

public class DeviceInfo extends UnityComponent {

    public static DeviceInfo _instance;
    public static Object instance(){
        _instance = new DeviceInfo();
        return _instance;
    }

    private final com.zendesk.sdk.model.DeviceInfo deviceInfo;

    public DeviceInfo() {
        this.deviceInfo = new com.zendesk.sdk.model.DeviceInfo(getActivity().getApplicationContext());
    }

    public String getAsString(){
        return deviceInfo.toString();
    }

    public String getAsDictionary(){
        return gson().toJson(deviceInfo.getDeviceInfoAsMap());
    }

    public String getModelManufacturer(){
        return deviceInfo.getModelManufacturer();
    }

    public String getVersionName(){
        return deviceInfo.getVersionName();
    }

    public String getModelDeviceName(){
        return deviceInfo.getModelDeviceName();
    }

    public String getModelName(){
        return deviceInfo.getModelName();
    }

    public int getVersionCode(){
        return deviceInfo.getVersionCode();
    }

    public int getTotalMemory(){
        Map<String, String> deviceMap = deviceInfo.getDeviceInfoAsMap();
        return Integer.valueOf(deviceMap.get("device_total_memory"));
    }

    public int getUsedMemory(){
        Map<String, String> deviceMap = deviceInfo.getDeviceInfoAsMap();
        return Integer.valueOf(deviceMap.get("device_used_memory"));
    }
}
