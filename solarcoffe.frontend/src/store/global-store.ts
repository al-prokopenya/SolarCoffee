import { make } from "vuex-pathify";
import { IInventoryTimeline } from "@/types/InventoryGraph";
import { InventoryService } from "@/services/inventory-service";

class GlobalStore {
  snapshotTimeline: IInventoryTimeline = {
    productInventorySnaphots: [],
    timeline: [],
  };

  isTimelineBuilt: boolean = false;
}

const state = new GlobalStore();

const mutations = make.mutations(state);

const actions = {
  async assignSnapshots({ commit }) {
    const inventoryService = new InventoryService();
    const res = await inventoryService.getSnapshotsHistory();

    const timeline: IInventoryTimeline = {
      productInventorySnaphots: res.productInventorySnaphots,
      timeline: res.timeline,
    };

    commit("SET_SNAPSHOT_TIMELINE", timeline);
    commit("SET_IS_TIMELINE_BUILT", true);
  },
};

const getters = {};

export default {
  state,
  mutations,
  actions,
  getters,
};
