import finOpsApiClient from "@/axios/FinOpsAxios";
import {Data} from "@/types/Data.ts";

export async function getDepDirectory() {
  const response = await finOpsApiClient.get(`getDepDirectory/`);

  return response.data;
}

export async function AddData(data: Data) {
  const response = await finOpsApiClient.post(`AddData/`, data);
  return response.data;
}

