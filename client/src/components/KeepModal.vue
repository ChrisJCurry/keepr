<template>
  <div class="modal fade keep-modal"
       tabindex="-1"
       :id="'keep-modal-' + keepProp.id"
       role="dialog"
       aria-labelledby="myLargeModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-xl modal-size">
      <div class="modal-content">
        <div class="modal-body m-1">
          <div class="row d-flex">
            <div class="col-6">
              <img :src="keepProp.img" />
            </div>
            <div class="col-6 pl-0 ml-0 mt-2">
              <div class="row d-none d-md-flex">
                <div class="col-3 offset-2">
                  <span class="d-flex">
                    <i class="gg-eye mr-2"></i>
                    <p>0</p>
                  </span>
                </div>
                <div class="col-3">
                  <span class="d-flex">
                    <i class="gg-bookmark mr-3"></i>
                    <p>0</p>
                  </span>
                </div>
                <div class="col-3">
                  <span class="d-flex">
                    <i class="gg-share mr-4 mt-2"></i>
                    <p>0</p>
                  </span>
                </div>
              </div>
              <div class="row text-center mt-3">
                <h2>
                  {{ keepProp.name }}
                </h2>
              </div>
              <div class="row text-center keep-description">
                <h2>
                  {{ keepProp.description }}
                </h2>
              </div>
              <div class="keep-description-border"></div>
              <div class="row keep-footer p-0 m-0">
                <div class="col-6">
                  <div class="dropdown">
                    <button class="btn btn-secondary dropdown-toggle"
                            type="button"
                            id="dropdownMenuButton"
                            data-toggle="dropdown"
                            aria-haspopup="true"
                            aria-expanded="false"
                    >
                      Add to Vault
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                      <a class="dropdown-item" href="#">Action</a>
                      <a class="dropdown-item" href="#">Another action</a>
                      <a class="dropdown-item" href="#">Something else here</a>
                    </div>
                  </div>
                </div>
                <div class="col-3 offset-3">
                  <img :src="keepProp.creator.picture" class="keep-profile ml-lg-4">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>

export default {

  name: 'KeepModal',
  props: {
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    return {
    }
  }
}
</script>

<style scoped lang="scss">
i {
  position: relative;
  top: 1px;
  left: 0px;
}

.keep-profile {
  height: 2rem;
  width: 2rem;
}

.modal-size {
  max-width: 95%;
}

.keep-footer {
  position: absolute;
  bottom: 1%;
  width: 100%;
}

.keep-description {
  margin-top: 5rem;
}
.keep-description-border {
  position: absolute;
  margin-top: 1rem;
  left: 25%;
  width: 50%;
  border-bottom: 1px solid black;
}
.modal-body {
  padding: 0 !important;
}
.modal-dialogue {
  padding: 0 !important;
}

.modal-content {
  border-radius: 2% !important;
}

img {
  height: 30rem;
  width: 100%;
  /* border-top-left-radius: 3%;
  border-bottom-left-radius: 3%; */
  border-radius: 2%;
}
</style>
