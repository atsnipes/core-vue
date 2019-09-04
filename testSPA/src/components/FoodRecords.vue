<template>
  <div class="container-fluid mt-4">
    <h1 class="h1">Food Records</h1>
    <b-alert :show="loading" variant="info">Loading...</b-alert>
    <b-row>
      <b-col>
        <table class="table table-striped">
          <thead>
            <tr>
              <th>Name</th>
              <th>Value</th>
              <th>Created Date</th>
              <th>Updated Date</th>
              <th>&nbsp;</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="record in records" :key="record.id">
              
              <td>{{ record.name }}</td>
              <td>{{ record.value }}</td>
              <td>{{ record.createdDate }}</td>
              <td>{{ record.updatedDate }}</td>
              <td class="text-right">
                <a href="#" @click.prevent="updatePlant(record)">Edit</a> -
                <a href="#" @click.prevent="deletePlant(record.id)">Delete</a>
              </td>
            </tr>
          </tbody>
        </table>
      </b-col>
      <b-col lg="4">
        <b-card :title="(model.id ? 'Edit Plant ID#' + model.id : 'New Plant Record')">
          <form @submit.prevent="createPlant">
            <b-form-group label="Name">
              <b-form-input type="text" v-model="model.name"></b-form-input>
            </b-form-group>
            <b-form-group label="Value">
              <b-form-input rows="4" v-model="model.value" type="number"></b-form-input>
            </b-form-group>
            <div>
              <b-btn type="submit" variant="success">Save Record</b-btn>
            </div>
          </form>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script>
  import api from '../api/PlantRecordsApiClient';

  export default {
    data() {
      return {
        loading: false,
        records: [],
        model: {}
      };
    },
    async created() {
      this.getAll()
    },
    methods: {
      async getAll() {
        this.loading = true;

        try {
          this.records = await api.getAll();
        } 
        catch(ex) {
            console.log(ex);
        }
        finally {
          this.loading = false;
        };
      },
      async updatePlant(plantToUpdate) {
        // We use Object.assign() to create a new (separate) instance
        this.model = Object.assign({}, plantToUpdate)
      },
      async createPlant() {
        const isUpdate = !!this.model.id;

        if (isUpdate) {
          await api.update(this.model.id, this.model)
        } else {
          await api.create(this.model)
        }

        // Clear the data inside of the form
        this.model = {}

        // Fetch all records again to have latest data
        await this.getAll()
      },
      async deletePlant(id) {
        if (confirm('Are you sure you want to delete this record?')) {
          // if we are editing a food record we deleted, remove it from the form
          if (this.model.id === id) {
            this.model = {}
          }

          await api.delete(id);
        }
      }
    }
  }
</script>