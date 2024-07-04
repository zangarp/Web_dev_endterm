<script setup lang="ts">
import {onMounted, ref} from "vue";
import {AddData, getDepDirectory} from "@/api/finOpsAPI.ts";
import {Data} from "@/types/Data.ts";
import {Department} from "@/types/Department.ts";


const files = ref();
const isModalVisible = ref<boolean>(false);
const data = ref<Data>({} as Data);
const selectedBusinessOwners = ref();
const businessOwners = ref();
const selectedDepartment = ref<Department>();
const departments = ref<Department[]>([]);
const handleFileUpload = (event: Event) => {
  const target = event.target as HTMLInputElement;
  if (target.files) {
    files.value = Array.from(target.files);
  }
};
const fetchData = async ()=>{
  departments.value = await getDepDirectory();
  console.log( departments.value);

}
onMounted(async()=>{
  await fetchData();
});
const createData = async (data: Data) => {
  try {
    await AddData(data);
  } catch (e) {
    console.error(e);
  }
}
</script>


<template>
  <div>
    <div class="card flex justify-center" style="margin-top: 20px; margin-left: 20px;">
      <Button label="Создать план" @click="isModalVisible = true" style="background-color: #f05e22; color: white; border: none; padding: 7px 50px; cursor: pointer; border-radius: 4px;" />
    </div>
    <Dialog v-model:visible="isModalVisible" modal header="Создание план" :style="{ width: '40%' }">

      <label for="planName" class="col-form-label fst-italic">Название плана</label>
      <input v-model="data.name" type="text" class="form-control" id="planName" placeholder="Введите название" />

      <label for="taskDescription" class="col-form-label fst-italic">Описание</label>
      <textarea v-model="data.description" class="form-control" id="taskDescription" placeholder="Введите описание" rows="5"></textarea>

      <label for="businessOwner" class="col-form-label fst-italic">Бизнес владелец</label>
      <MultiSelect v-model="selectedBusinessOwners" :options="businessOwners" optionLabel="name" filter placeholder="Выберите бизнес владельца" class="w-full md:w-80" />

      <label for="responsibleDepartment" class="col-form-label fst-italic">Отв. подразделение</label>
      <MultiSelect v-model="selectedDepartment" :options="departments" optionLabel="fullName" filter placeholder="Выберите подразделение" class="w-full md:w-80" />
      <div class="tw-mt-[10px]">
        <checkbox v-model="data.isBlockDivision"/>
        <label for="isBlockDivision">Признак разделение по блоку</label>
      </div>
<!--      <div class="radio-group">-->
<!--        <div>-->
<!--          <input type="checkbox" id="favoritismToggle" v-model="favoritism" />-->
<!--          <label for="favoritismToggle">Признак разделение по блоку</label>-->
<!--        </div>-->
<!--      </div>-->

      <label class="col-form-label fst-italic">Вложить файлы</label>
      <input type="file" id="fileUpload" @change="handleFileUpload" multiple class="form-control-file" />

      <button type="submit" @click="createData(data)" class="btn-create">Создать</button>
    </Dialog>

<!--    <div class="created-plans" v-if="createdPlans.length">-->
<!--      <h3>Созданные планы</h3>-->
<!--      <ul>-->
<!--        <li v-for="(name, status) in createdPlans" :key="index">-->
<!--          <p><strong>Название плана:</strong> {{ plan.name }}</p>-->
<!--          <p><strong>Статус:</strong> {{ plan.status }}</p>-->
<!--        </li>-->
<!--      </ul>-->
<!--    </div>-->

<!--    <div v-if="isModalVisible" class="modal-backdrop">-->
<!--      <div class="modal">-->
<!--        <div class="modal-header">-->
<!--          <h3>Создать новый план</h3>-->
<!--          <button type="button" @click="toggle" class="btn-close" aria-label="Закрыть">x</button>-->
<!--        </div>-->
<!--        <div class="modal-body">-->
<!--          <form @submit.prevent="createTask">-->
<!--            <label for="planName" class="col-form-label fst-italic">Название плана</label>-->
<!--            <input v-model="planName" type="text" class="form-control" id="planName" placeholder="Введите название" />-->

<!--            <label for="taskDescription" class="col-form-label fst-italic">Описание</label>-->
<!--            <textarea v-model="taskDescription" class="form-control" id="taskDescription" placeholder="Введите описание" rows="5"></textarea>-->

<!--            <label for="businessOwner" class="col-form-label fst-italic">Бизнес владелец</label>-->
<!--            <MultiSelect v-model="selectedBusinessOwners" :options="businessOwners" optionLabel="name" filter placeholder="Выберите бизнес владельца" class="w-full md:w-80" />-->

<!--            <label for="responsibleDepartment" class="col-form-label fst-italic">Отв. подразделение</label>-->
<!--            <MultiSelect v-model="selectedDepartments" :options="departments" optionLabel="name" filter placeholder="Выберите подразделение" class="w-full md:w-80" />-->

<!--            <div class="radio-group">-->
<!--              <div>-->
<!--                <input type="checkbox" id="favoritismToggle" v-model="favoritism" />-->
<!--                <label for="favoritismToggle">Признак разделение по блоку</label>-->
<!--              </div>-->
<!--            </div>-->

<!--            <label class="col-form-label fst-italic">Вложить файлы</label>-->
<!--            <input type="file" id="fileUpload" @change="handleFileUpload" multiple class="form-control-file" />-->

<!--            <button type="submit" class="btn-create">Создать</button>-->
<!--          </form>-->
<!--        </div>-->
<!--      </div>-->
<!--    </div>-->
  </div>
</template>


<style scoped>
.modal-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal {
  background: white;
  border-radius: 8px;
  padding: 20px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border-bottom: 1px solid #e5e5e5;
  padding-bottom: 10px;
}

.modal-body {
  padding-top: 10px;
}

.form-control {
  width: 100%;
  padding: 10px;
  border-radius: 4px;
  border: 1px solid #ccc;
  margin-bottom: 10px;
}

.form-control-file {
  width: 100%;
  padding: 10px;
  border-radius: 4px;
  border: 1px solid #ccc;
  margin-bottom: 10px;
}

.btn-create {
  background-color: #f05e22;
  color: white;
  border: none;
  padding: 7px 20px;
  cursor: pointer;
  border-radius: 4px;
  margin-top: 10px;
}

.btn-create:hover {
  background-color: #e5531d;
}

.col-form-label {
  display: block;
  margin-bottom: 5px;
}

.radio-group {
  margin-bottom: 10px;
}

.favoritism-indicator {
  margin-top: 10px;
}
</style>

