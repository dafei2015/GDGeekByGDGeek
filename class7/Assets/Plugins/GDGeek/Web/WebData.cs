using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
namespace GDGeek{
	public class WebData{
		private string uuid_ = null;
		private string hash_ = null;
		private string sugar_ = null;
		private string server_ = null;
		
		public void setup(string uuid, string hash, string sugar, string server){
			this.uuid_ = uuid;
			this.hash_ = hash;
			this.sugar_ = sugar;
			this.server_ = server;
		}

		
		public string uuid{
			get{
				return uuid_;
			}
			
		}


		
		public string server{
			get{
				return server_;
			}
			
		}

		
		
		public string hash{
			get{
				return hash_;
			}
			
		}




		public string sugar{
			get{
				return sugar_;
			}

		}

	};
}